using RogueLike.CoreObjects;
using RogueLike.Physics.Collisions;
using System.Collections.Generic;

namespace RogueLike.Physics.QuadtreeSpace
{
    public class Quadtree
    {
        private int MAX_OBJECTS = 10;
        private int MAX_LEVELS = 5;

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
        }

        public void Clear()
        {
            Objects.Clear();

            for(int i = 0; i < Nodes.Length; i++)
            {
                if(Nodes[i] != null)
                {
                    Nodes[i].Clear();
                    Nodes[i] = null;
                }
            }
        }

        private void Split()
        {
            int width = (int)(Bounds.Width / 2);
            int height = (int)(Bounds.Height / 2);
            int x = (int)Bounds.Position.X;
            int y = (int)Bounds.Position.Y;

            Nodes[0] = new Quadtree(Level + 1, new Rektangle(x + width, y, width, height));
            Nodes[1] = new Quadtree(Level + 1, new Rektangle(x, y, width, height));
            Nodes[2] = new Quadtree(Level + 1, new Rektangle(x, y + height, width, height));
            Nodes[3] = new Quadtree(Level + 1, new Rektangle(x + width, y + height, width, height));
        }

        private int GetIndex(BaseObject obj)
        {
            int index = -1;
            double verticalMidpoint = Bounds.Position.X + (Bounds.Width / 2);
            double horizontalMidpoint = Bounds.Position.Y + (Bounds.Height / 2);

            // Object can completely fit within the top quadrants
            bool topQuadrant = (obj.Collider.Position.Y < horizontalMidpoint 
                                && obj.Collider.Position.Y + obj.Collider.Height < horizontalMidpoint);
            // Object can completely fit within the bottom quadrants
            bool bottomQuadrant = (obj.Collider.Position.Y > horizontalMidpoint);

            // Object can completely fit within the left quadrants
            if (obj.Collider.Position.X < verticalMidpoint 
                && obj.Collider.Position.X + obj.Collider.Width < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            // Object can completely fit within the right quadrants
            else if (obj.Collider.Position.X > verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }

            return index;
        }

        public void Insert(BaseObject obj)
        {
            if (Nodes[0] != null)
            {
                int index = GetIndex(obj);

                if (index != -1)
                {
                    Nodes[index].Insert(obj);

                    return;
                }
            }

            Objects.Add(obj);

            if (Objects.Count > MAX_OBJECTS && Level < MAX_LEVELS)
            {
                if (Nodes[0] == null)
                {
                    Split();
                }

                int i = 0;
                while (i < Objects.Count)
                {
                    int index = GetIndex(Objects[i]);
                    if (index != -1)
                    {
                        Nodes[index].Insert(Objects[i]);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        public List<BaseObject> Retrieve(List<BaseObject> returnObjects, BaseObject obj)
        {
            int index = GetIndex(obj);
            if (index != -1 && Nodes[0] != null)
            {
                Nodes[index].Retrieve(returnObjects, obj);
            }

            returnObjects.AddRange(Objects);

            return returnObjects;
        }
    }
}