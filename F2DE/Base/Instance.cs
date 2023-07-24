﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F2DE.Base.Builders;
using F2DE.Base.Interfaces;

namespace F2DE.Base
{
    internal abstract class Instance : ITickable, IRenderTickable, IDisposable
    {
        public Game game;

        public Instance(Game game)
        {
            this.game = game;
        }

        private List<Entity> entities = new List<Entity>();

        public void Tick()
        {
            foreach (var entity in entities)
            {
                entity.Tick();
            }
        }

        public Entity Add(EntityBuilder builder)
        {
            var entity = builder.Build(this);
            entities.Add(entity);
            entity.Post();
            return entity;
        }

        public void RenderTick(string layer)
        {
            foreach (var entity in entities)
            {
                entity.RenderTick(layer);
            }
        }

        public void Dispose()
        {
            foreach (var entity in entities)
            {
                entity.Dispose();
            }
        }

        public void Destroy(Entity entity)
        {
            entities.Remove(entity);
            entity.Dispose();
        }
    }
}