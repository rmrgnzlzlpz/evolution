using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Domain
{
    public abstract class BaseEntity
    {
    }

    public abstract class Entity : BaseEntity, IEntity
    {
        public long Id { get; set; }

        public Entity(IDataRecord row)
        {
            Id = long.Parse(row["id"].ToString());
        }

        public Entity()
        {

        }
    }
}
