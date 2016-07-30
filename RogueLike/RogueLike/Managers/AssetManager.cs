using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace RogueLike.Managers
{
    public static class AssetManager
    {
        #region Const File Paths

        /// <summary>
        /// The file path relative to our 'Content' folder for the directory we keep our sprites in
        /// </summary>
        private const string SpritesDirectory = "Sprites";

        #endregion

        #region Properties and Fields

        private static Dictionary<string, Texture2D> Sprites { get; set; }

        #endregion

        /// <summary>
        /// Iterate through all of the subdirectories of the Sprites directory and load each sprite.
        /// Then cache in the dictionary using the relative path from the Sprites directory as the key.
        /// e.g. a file in Content\\Sprites\\SubDirectory\\TestImage.png would be saved as SubDirectory\\TestImage.
        /// </summary>
        /// <param name="content"></param>
        public static void LoadSprites(ContentManager content)
        {
            // Make sure we initialise the dictionary otherwise bad things will happen
            Sprites = new Dictionary<string, Texture2D>();

            // Make sure the sprites directory exists otherwise we should not be calling this function
            string spritesDirectoryPath = Path.Combine(content.RootDirectory, SpritesDirectory);
            Debug.Assert(Directory.Exists(spritesDirectoryPath));

            for (FileInfo file in new DirectoryInfo(spritesDirectoryPath).GetFiles("*.xnb")
            {

            }
        }
    }
}