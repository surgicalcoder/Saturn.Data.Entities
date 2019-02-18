﻿using System;

namespace GoLive.Saturn.Data.Entities
{
    public abstract class Entity : IEquatable<Entity>
    {
        public virtual string Id { get; set; }
        public virtual long? Version { get; set; }

        public bool Equals(Entity other)
        {
            return other != null && Id == other.Id;
        }
    }
}
