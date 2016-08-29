﻿using RogueLike.CoreObjects;
using RogueLike.Physics.Collisions;
using System.Collections.Generic;

namespace RogueLike.Physics.QuadtreeSpace
{
    public class Quadtree
    {
        private const int MAX_LEVELS = 5;

        private int Level;
        private Rektangle Bounds;
        private List<BaseObject> Objects;
        private Quadtree[] Nodes;
        
        public Quadtree(int level, Rektangle bounds)
        {
            Level = level;
            Bounds = bounds;
            Objects = new List<BaseObject>();
            Nodes = new Quadtree[4];

            if (Level < MAX_LEVELS && Nodes == null)
            {
                Split();
            }
        }

        private void Split()
        {
            float width = Bounds.Width / 2;
            float height = Bounds.Height / 2;
            float x = Bounds.Position.X;
            float y = Bounds.Position.Y;

            Nodes[0] = new Quadtree(Level + 1, new Rektangle(x, y, width, height));
            Nodes[1] = new Quadtree(Level + 1, new Rektangle(x + width, y, width, height));
            Nodes[2] = new Quadtree(Level + 1, new Rektangle(x, y + height, width, height));
            Nodes[3] = new Quadtree(Level + 1, new Rektangle(x + width, y + height, width, height));
        }

        public void Insert(BaseObject obj)
        {
            if(!obj.Collider.CollidedWithRectangle(Bounds))
            {
                return;
            }

            Objects.Add(obj);

            if(Nodes != null)
            {
                foreach (Quadtree node in Nodes)
                {
                    node.Insert(obj);
                }
            }
        }

        public void Retrieve(List<BaseObject> returnObjects, BaseObject obj)
        {
            if (Nodes != null)
            {
                foreach(Quadtree node in Nodes)
                {
                    node.Retrieve(returnObjects, obj);
                }
            }
            else if(Objects.Contains(obj))
            {
                returnObjects.AddRange(Objects);
            }
        }

        public void Clear()
        {
            Objects.Clear();

            for (int i = 0; i < Nodes.Length; i++)
            {
                if (Nodes[i] != null)
                {
                    Nodes[i].Clear();
                    Nodes[i] = null;
                }
            }
        }
    }
}