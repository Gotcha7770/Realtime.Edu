﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using LiteDB;
using LiteDB.Realtime;
using NUnit.Framework;
using Realtime.Edu.Core.Models;

namespace Realtime.Edu.Tests
{
    [TestFixture]
    public class DBTest
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // BsonMapper.Global.RegisterType(x => new BsonDocument
            //     {
            //         ["_id"] = x.Key,
            //         ["Entities"] = x.Entities.ToBsonArray()
            //     },
            //     x => new Settings(x["_id"],
            //         x["Entities"].AsArray.Select(i => BsonMapper.Global.Deserialize<Property>(i))));
            
            // BsonMapper.Global.Entity<Settings>()
            //     .Ctor(x =>
            //     {
            //         var result = new Settings();
            //         //x.ForEach();
            //         return result;
            //     });
            //.DbRef(x => x.Trajectories, "trajectories");
        }

        [Test]
        public void Serialization()
        {
            var property = new Property {Value = "Test"};
            var settings = new Settings(Guid.NewGuid());
            settings.Add(property);
            
            var well = new Well(Guid.NewGuid(), "Main");
            well.Add(new Trajectory(Guid.NewGuid(), "1"));
            well.Add(new Trajectory(Guid.NewGuid(), "2"));
            well.Add(new Trajectory(Guid.NewGuid(), "3"));

            var document = BsonMapper.Global.ToDocument(property);
            object obj = BsonMapper.Global.ToObject<Property>(document);
            
            document = BsonMapper.Global.ToDocument(settings);
            var bson = BsonMapper.Global.Serialize(settings);
            obj = BsonMapper.Global.ToObject<Settings>(document);
            
            document = BsonMapper.Global.ToDocument(well);
            obj = BsonMapper.Global.ToObject<Well>(document);
        }

        [Test]
        public void QueryWithoutRefs()
        {
            using (var db = new RealtimeLiteDatabase(new MemoryStream()))
            {
                var main = new Well(Guid.NewGuid(), "MainWell");
                main.Add(new Trajectory(Guid.NewGuid(), "MainTraj"));
                var pilot = new Well(Guid.NewGuid(), "PilotWell");
                pilot.Add(new Trajectory(Guid.NewGuid(), "PilotTraj"));
                
                var wells = db.GetCollection<Well>("wells");
                wells.Insert(main);
                wells.Insert(pilot);

                var one = wells.FindById(main.Key);
                var all = wells.FindAll().ToArray();
                var names = wells.Query().Select(x => x.Name).ToArray();
                var firstTraj = wells.Query().Select(x => x.Trajectories.First()).ToArray();
                var children1 = wells.Query().Select(x => x.Trajectories).ToArray();
                var children2 = wells.Query().Select("Trajectories").ToDocuments();
            }
        }

        //[Test]
        // public void GenericsTest()
        // {
        //     using (var db = new RealtimeLiteDatabase(new MemoryStream()))
        //     {
        //         var proj = new EntityComponent("Project");
        //         var pp = new EntityComponent("PorePressure");
        //         var settings = new EntityComponent<Settings>("PPSettings")
        //         {
        //             Data = new Settings
        //             {
        //                 new Property {Value = "PPG"},
        //                 new Property {Value = "> 1000"}
        //             }
        //         };
        //         pp.Components.Add(settings);
        //         proj.Components.Add(pp);
        //
        //         var components = db.GetCollection<EntityComponent>("components");
        //         components.Insert(proj);
        //
        //         var result = components.FindById(proj.Key);
        //     }
        // }

        // [Test]
        // public void QueryWithRefs()
        // {
        //     using (var db = new RealtimeLiteDatabase(new MemoryStream()))
        //     {
        //         var well = new Well2("MainWell");
        //         var trajectory = new Trajectory("Traj1");
        //         well.Trajectories.Add(trajectory);
        //
        //         var trajectories = db.GetCollection<Trajectory>("trajectories");
        //         trajectories.Insert(trajectory);
        //
        //         var wells = db.GetCollection<Well2>("wells");
        //         wells.Insert(well);
        //
        //         var res = db.GetCollection<Well1>()
        //             .Query()
        //             .Select(x => x.Trajectories)
        //             .ToArray();
        //     }
        // }

        [Test]
        public void WellObservable()
        {
            List<Well> res;
            using (var db = new RealtimeLiteDatabase(new MemoryStream()))
            using (var _ = db.Realtime.Collection<Well>("wells")
                .Subscribe(x => res = x))
            {
                var well = new Well(Guid.NewGuid(), "MainWell");
                well.Add(new Trajectory(Guid.NewGuid(), "Traj1"));
                
                var wells = db.GetCollection<Well>("wells");
                wells.Insert(well);

                well.Add(new Trajectory(Guid.NewGuid(), "Traj2"));
                wells.Update(well);
            }
        }

        [Test]
        public void TrajectoryObservable()
        {
            Trajectory[] res;
            using (var db = new RealtimeLiteDatabase(new MemoryStream()))
                //using(var _ = db.Realtime.Collection<Trajectory>("trajectories").Subscribe(x => res = x))
            using (var _ = db.Realtime.Collection<Well>("wells")
                .Select(x => x.SelectMany(w => w.Trajectories))
                .Subscribe(x => res = x.ToArray()))
            {
                var well = new Well(Guid.NewGuid(), "MainWell");
                well.Add(new Trajectory(Guid.NewGuid(), "Traj1"));
                var wells = db.GetCollection<Well>("wells");
                wells.Insert(well);

                well.Add(new Trajectory(Guid.NewGuid(), "Traj2"));
                wells.Update(well);
            }
        }

        // [Test]
        // public void SingleEntity()
        // {
        //     var scheduler = new TestScheduler();
        //     using (var db = new RealtimeLiteDatabase(new MemoryStream()))
        //     {
        //         var well = new Well1();
        //         var res = scheduler.Start(() => db.Realtime.Collection<Well1>("wells")
        //             .Id(well.Key));
        //         var wells = db.GetCollection<Well1>("wells");
        //
        //         well.Trajectories.Add(new Trajectory("Traj1"));
        //         wells.Insert(well);
        //
        //         well.Trajectories.Add(new Trajectory("Traj2"));
        //         wells.Update(well);
        //     }
        // }
    }
}