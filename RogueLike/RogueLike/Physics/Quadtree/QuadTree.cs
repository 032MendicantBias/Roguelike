using Microsoft.Xna.Framework;
using RogueLike.CoreObjects;
using RogueLike.Managers;
using RogueLike.Physics.Collisions;
using System;
using System.Collections.Generic;

namespace RogueLike.Physics.Quadtree
{
    public class QuadTree
    {
        private int NumSquaresX { get; set; }
        private int NumSquaresY { get; set; }

        private List<BaseObject> objects;
        private GridSquare[,] squares;

        public QuadTree(List<BaseObject> objects) 
            : this(objects, ScreenManager.Instance.ScreenDimensions) { }

        public QuadTree(List<BaseObject> objects, Vector2 Dimensions)
        {
            this.objects = objects;
            NumSquaresX = 16;
            NumSquaresY = 16;
        }

        public List<Tuple<BaseObject, BaseObject>> Calculate()
        {
            squares = new GridSquare[NumSquaresY, NumSquaresX];

            LoadSquares();
            List<Tuple<BaseObject, BaseObject>> collisions = GetCollisions();
            return collisions;
        }

        public List<Tuple<BaseObject, BaseObject>> GetCollisions()
        {
            List<Tuple<BaseObject, BaseObject>> collisions = new List<Tuple<BaseObject, BaseObject>>();
            
            foreach(GridSquare square in squares)
            {
                for(int i = 0; i < square.Objects.Count; i++)
                {
                    for(int j = i + 1; j < square.Objects.Count; j++)
                    {
                        if(CollisionsManager.CheckCollision(
                            square.Objects[i].Collider, square.Objects[j].Collider))
                        {
                            collisions.Add(
                                new Tuple<BaseObject, BaseObject>(square.Objects[i], square.Objects[j]));
                        }
                    }
                }
            }

            return null;
        }

        public void LoadSquares()
        {
            for (int i = 0; i < NumSquaresX; i++)
            {
                for (int j = 0; j < NumSquaresY; j++)
                {
                    foreach (BaseObject obj in objects)
                    {
                        if (CollisionsManager.CheckCollision(squares[i, j], obj.Collider))
                        {
                            squares[i, j].Add(obj);
                        }
                    }
                }
            }
        }

        private class GridSquare : Rektangle
        {
            public List<BaseObject> Objects { get; private set; }

            public GridSquare(Vector2 position, Vector2 dimensions)
                : base(position, dimensions) 
            {
                Objects = new List<BaseObject>();
            }

            public void Add(BaseObject obj)
            {
                Objects.Add(obj);
            }
        }
    }
}
