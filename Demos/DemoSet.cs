﻿using DemoContentLoader;
using DemoRenderer;
using Demos.Demos;
using Demos.SpecializedTests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demos
{
    /// <summary>
    /// Constructs a demo from the set of available demos on demand.
    /// </summary>
    public class DemoSet
    {
        struct Option
        {
            public string Name;
            public Func<ContentArchive, Camera, Demo> Builder;
        }

        List<Option> options = new List<Option>();
        void AddOption<T>() where T : Demo, new()
        {
            options.Add(new Option
            {
                Builder = (content, camera) =>
                {
                    //Note that the actual work is done in the Initialize function rather than a constructor.
                    //The 'new T()' syntax actually uses reflection and repackages exceptions in an inconvenient way.
                    //By using Initialize instead, the stack trace and debugger will go right to the source.
                    var demo = new T();
                    demo.Initialize(content, camera);
                    return demo;
                },
                Name = typeof(T).Name
            });
        }

        public DemoSet()
        {
            AddOption<MeshDemo>();
            AddOption<PyramidDemo>();
            AddOption<BlockChainDemo>();
            AddOption<ShapePileDemo>();
            AddOption<BasicRagdollDemo>();
            AddOption<ClothLatticeDemo>();
            AddOption<CompoundTestDemo>();
            AddOption<SphereBlobTestDemo>();
            AddOption<RayCastingDemo>();
            AddOption<SweepDemo>();
            AddOption<FountainStressTestDemo>();
        }

        public int Count { get { return options.Count; } }

        public string GetName(int index)
        {
            return options[index].Name;
        }

        public Demo Build(int index, ContentArchive content, Camera camera)
        {
            return options[index].Builder(content, camera);
        }
    }
}
