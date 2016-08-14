using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace RogueLike
{
    /// <summary>
    /// A class to load and cache all the assets (including XML data).
    /// This will be done on startup in the Game1 LoadContent function
    /// </summary>
    public static class AssetManager
    {
        #region File Paths

        private const string SpriteFontsPath = "SpriteFonts\\";
        private const string SpritesPath = "Sprites\\";
        private const string EffectsPath = "Effects\\";
        private const string DataPath = "Data\\";
        public const string OptionsPath = "Options\\Options.xml";

        #endregion

        #region Default Assets

        public const string MouseTextureAsset = "UI\\Cursor";
        
        #endregion

        #region Properties

        private static Dictionary<string, SpriteFont> SpriteFonts = new Dictionary<string, SpriteFont>();
        private static Dictionary<string, Texture2D> Sprites = new Dictionary<string, Texture2D>();
        private static Dictionary<string, Effect> Effects = new Dictionary<string, Effect>();

        #endregion

        /// <summary>
        /// Loads all the assets from the default spritefont, sprites and data directories.
        /// Formats them into dictionaries so that they can be obtained with just the filename (minus the filetype)
        /// </summary>
        /// <param name="content"></param>
        public static void LoadAssets(ContentManager content)
        {
            SpriteFonts = Load<SpriteFont>(content, SpriteFontsPath);
            Sprites = Load<Texture2D>(content, SpritesPath);
            Effects = Load<Effect>(content, EffectsPath);
        }

        /// <summary>
        /// Loads all the assets of an inputted type that exist in our Content folder
        /// </summary>
        /// <typeparam name="T">The type of asset to load</typeparam>
        /// <param name="content">The ContentManager we will use to load our content</param>
        /// <param name="path">The path of the assets we wish to load</param>
        /// <returns>Returns the dictionary of all loading content</returns>
        private static Dictionary<string, T> Load<T>(ContentManager content, string path)
        {
            Dictionary<string, T> objects = new Dictionary<string, T>();

            string directoryPath = Path.Combine(content.RootDirectory, path);

            if (Directory.Exists(directoryPath))
            {
                string[] files = Directory.GetFiles(directoryPath, "*.xnb*", SearchOption.AllDirectories);
                for (int i = 0; i < files.Length; i++)
                {
                    // Remove the directoryPath from the start of the string
                    files[i] = files[i].Remove(0, directoryPath.Length);

                    // Remove the .xnb at the end
                    files[i] = files[i].Split('.')[0];

                    try
                    {
                        // TODO Use LoadFromDisc
                        objects.Add(files[i], content.Load<T>(path + files[i]));
                    }
                    catch { Debug.Fail("Problem loading asset: " + files[i]); }
                }
            }

            return objects;
        }
        
        ///// <summary>
        ///// A wrapper for loading content directly using the ContentManager.
        ///// Should only be used as a last resort.
        ///// </summary>
        ///// <typeparam name="T">The type of content to load</typeparam>
        ///// <param name="path">The full path of the object from the ContentManager directory e.g. Sprites\\UI\\Cursor</param>
        ///// <param name="extension">The extension of the file we are trying to load - used for error checking</param>
        ///// <returns>The loaded content</returns>
        //private static T LoadFromDisc<T>(string path)
        //{
        //    Debug.Fail("TODO Use stored ContentManager rather than use it as a parameter");
        //    T asset = default(T);

        //    // Because File.Exists relies on extensions and we do not use extensions, we cannot do a test here.
        //    // We use try catch here instead.
        //    // Ouch - I know, but there's no real nice workaround, unless we can check without extensions
        //    try
        //    {
        //        asset = content.Load<T>(path);
        //    }
        //    catch
        //    {
        //        asset = default(T);
        //    }

        //    DebugUtils.AssertNotNull(asset);
        //    return asset;
        //}
        
        #region Utility Functions

        /// <summary>
        /// Get a loaded SpriteFont
        /// </summary>
        /// <param name="path">The full path of the SpriteFont, e.g. "DefaultSpriteFont"</param>
        /// <returns>Returns the sprite font</returns>
        public static SpriteFont GetSpriteFont(string path)
        {
            SpriteFont spriteFont;

            if (!SpriteFonts.TryGetValue(path, out spriteFont))
            {
                // TODO Reimplement
                //spriteFont = LoadFromDisc<SpriteFont>(SpriteFontsPath + path);
            }

            DebugUtils.AssertNotNull(spriteFont);
            return spriteFont;
        }

        /// <summary>
        /// Get a pre-loaded sprite
        /// </summary>
        /// <param name="path">The relative path of the Sprite from the Sprites directory, e.g. "UI\\Cursor"</param>
        /// <returns>Returns the texture</returns>
        public static Texture2D GetSprite(string path)
        {
            Texture2D sprite;

            if (!Sprites.TryGetValue(path, out sprite))
            {
                // TODO Reimplement
                //sprite = LoadFromDisc<Texture2D>(SpritesPath + path);
            }

            DebugUtils.AssertNotNull(sprite);
            return sprite;
        }

        /// <summary>
        /// Get a pre-loaded effect
        /// </summary>
        /// <param name="path">The full path of the Effect, e.g. "LightEffect"</param>
        /// <returns>Returns the effect</returns>
        public static Effect GetEffect(string path)
        {
            Effect effect;

            if (!Effects.TryGetValue(path, out effect))
            {
                // TODO Reimplement
                //effect = LoadFromDisc<Effect>(EffectsPath + path);
            }

            DebugUtils.AssertNotNull(effect);
            return effect;
        }

        #endregion
    }
}