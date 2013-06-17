using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using FarseerPhysics.Dynamics;

namespace TurkeySmash
{
    class Jeu : Screen
    {
        World world;
        HUD hud = new HUD();
        public static SoundEffect sonEspace = TurkeySmashGame.content.Load<SoundEffect>("Sons\\sonEspace");
        public SoundEffectInstance sonInstance = sonEspace.CreateInstance();
        Character[] personnages = new Character[4];
        Level level;
        float fixedMass = 0.22f;
        bool gameStarted = false;
        int compteurDebutDePartie = 0;
        AnimatedSprite compteur;

        private int i = 0;

        public Jeu()
        {
            state = SceneState.Active;
        }

        public override void Init()
        {
            world =new World(Vector2.UnitY * 20f);

            if (SelectionNiveau.niveauSelect == "level1")
                level = new Level(world, "Jeu\\level1\\background1", personnages, TurkeySmashGame.content);
            else
            {
                if (SelectionNiveau.niveauSelect == "level2")
                    level = new Level(world, "Jeu\\level2\\background2", personnages, TurkeySmashGame.content);
                else
                {
                    if (SelectionNiveau.niveauSelect == "level3")
                        level = new Level(world, "Jeu\\level3\\background3", personnages, TurkeySmashGame.content);
                    else
                        level = new Level(world, "Jeu\\level4\\background4", personnages, TurkeySmashGame.content);
                }
            }

            #region loadPersonnage

            /*
            #region Debug

            personnages[0] = new Joueur(world, level.spawnPoints[0], 1f, new Vector2(42, 55), PlayerIndex.Three, new AnimatedSpriteDef()
            {
                AssetName = "Jeu\\naruto",
                FrameRate = 60,
                FrameSize = new Point(88, 88),
                Loop = true,
                NbFrames = new Point(5, 1),
            });
            personnages[1] = new Joueur(world, level.spawnPoints[1], 1f, new Vector2(40, 50), Convert.Int2PlayerIndex(i + 1), new AnimatedSpriteDef()
            {
                AssetName = "Jeu\\sakura",
                FrameRate = 60,
                FrameSize = new Point(80, 80),
                Loop = true,
                NbFrames = new Point(6, 1),
            });

            #endregion
            */
            
            #region Selection persoonage

            foreach (string str in SelectionPersonnage.listPerso)
            {
                if (str != null)
                {
                    if (i <= ChoixNombrePersonnage.nombreJoueur - 1)
                    {
                        #region CreationJoueur;
                        if (str == "naruto")
                        {
                            personnages[i] = new Joueur(world, level.spawnPoints[i], 1f, new Vector2(42, 55), Convert.Int2PlayerIndex(i + 1), new AnimatedSpriteDef()
                            {
                                AssetName = "Jeu\\naruto",
                                FrameRate = 60,
                                FrameSize = new Point(88, 88),
                                Loop = true,
                                NbFrames = new Point(5, 1),
                            });
                        }
                        if (str == "sakura")
                        {
                            personnages[i] = new Joueur(world, level.spawnPoints[i], 1f, new Vector2(40, 50), Convert.Int2PlayerIndex(i + 1), new AnimatedSpriteDef()
                            {
                                AssetName = "Jeu\\sakura",
                                FrameRate = 60,
                                FrameSize = new Point(80, 80),
                                Loop = true,
                                NbFrames = new Point(6, 1),
                            });
                        }
                        if (str == "sai")
                        {
                            personnages[i] = new Joueur(world, level.spawnPoints[i], 1f, new Vector2(40, 50), Convert.Int2PlayerIndex(i + 1), new AnimatedSpriteDef()
                            {
                                AssetName = "Jeu\\sai",
                                FrameRate = 60,
                                FrameSize = new Point(80, 80),
                                Loop = true,
                                NbFrames = new Point(6, 1),
                            });
                        }
                        if (str == "suigetsu")
                        {
                            personnages[i] = new Joueur(world, level.spawnPoints[i], 1f, new Vector2(45, 58), Convert.Int2PlayerIndex(i + 1), new AnimatedSpriteDef()
                            {
                                AssetName = "Jeu\\suigetsu",
                                FrameRate = 60,
                                FrameSize = new Point(92, 92),
                                Loop = true,
                                NbFrames = new Point(6, 1),
                            });
                        }
                        if (str == "turkey")
                        {
                            personnages[i] = new Joueur(world, level.spawnPoints[i], 1f, new Vector2(25, 25), Convert.Int2PlayerIndex(i + 1), new AnimatedSpriteDef()
                            {
                                AssetName = "Jeu\\turkey",
                                FrameRate = 60,
                                FrameSize = new Point(46, 46),
                                Loop = true,
                                NbFrames = new Point(5, 1),
                            });
                        }
                        #endregion
                    }
                    else
                    {
                        #region CreationIA
                        if (str == "naruto")
                        {
                            personnages[i] = new IA(world, level.spawnPoints[i], 1f, new Vector2(42, 55), Convert.Int2PlayerIndex(i + 1), new AnimatedSpriteDef()
                            {
                                AssetName = "Jeu\\naruto",
                                FrameRate = 60,
                                FrameSize = new Point(88, 88),
                                Loop = true,
                                NbFrames = new Point(5, 1),
                            });
                        }
                        if (str == "sakura")
                        {
                            personnages[i] = new IA(world, level.spawnPoints[i], 1f, new Vector2(40, 50), Convert.Int2PlayerIndex(i + 1), new AnimatedSpriteDef()
                            {
                                AssetName = "Jeu\\sakura",
                                FrameRate = 60,
                                FrameSize = new Point(80, 80),
                                Loop = true,
                                NbFrames = new Point(6, 1),
                            });
                        }
                        if (str == "sai")
                        {
                            personnages[i] = new IA(world, level.spawnPoints[i], 1f, new Vector2(40, 50), Convert.Int2PlayerIndex(i + 1), new AnimatedSpriteDef()
                            {
                                AssetName = "Jeu\\sai",
                                FrameRate = 60,
                                FrameSize = new Point(80, 80),
                                Loop = true,
                                NbFrames = new Point(6, 1),
                            });
                        }
                        if (str == "suigetsu")
                        {
                            personnages[i] = new IA(world, level.spawnPoints[i], 1f, new Vector2(45, 58), Convert.Int2PlayerIndex(i + 1), new AnimatedSpriteDef()
                            {
                                AssetName = "Jeu\\suigetsu",
                                FrameRate = 60,
                                FrameSize = new Point(92, 92),
                                Loop = true,
                                NbFrames = new Point(6, 1),
                            });
                        }
                        if (str == "turkey")
                        {
                            personnages[i] = new Joueur(world, level.spawnPoints[i], 1f, new Vector2(25, 25), Convert.Int2PlayerIndex(i + 1), new AnimatedSpriteDef()
                            {
                                AssetName = "Jeu\\turkey",
                                FrameRate = 60,
                                FrameSize = new Point(46, 46),
                                Loop = true,
                                NbFrames = new Point(5, 1),
                            });
                        }
                        #endregion
                    }
                    personnages[i].body.Mass = fixedMass;
                    i = i + 1;
                }
            }
            #endregion
            

            #endregion

            #region initCompteur

            compteur = new AnimatedSprite(TurkeySmashGame.WindowMid - new Vector2(256, 256), new AnimatedSpriteDef()
            {
                AssetName = "Jeu\\compteurDebutDePartie",
                FrameRate = 60,
                FrameSize = new Point(512, 512),
                Loop = false,
                NbFrames = new Point(3, 0),
            });
            compteur.TimeBetweenFrame = 1000;
            compteur.color = Color.Orange;

            #endregion

            sonInstance.Volume = 0.5f;
            sonInstance.IsLooped = true;
            sonInstance.Resume();

            hud.Load(personnages);
        }

        public override void Update(GameTime gameTime, Input input)
        {
            if (input.Escape())
            {
                sonInstance.Pause();
                Basic.SetScreen(new Pause());
            }

            if (gameStarted)
            {
                compteur = null;
                level.Update(gameTime);
                hud.Update(gameTime, personnages);

                //Mise a jour du world en 30 FPS
                world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 60.0f))); 
            }
            else
            {
                compteur.Update(gameTime);
                if (compteurDebutDePartie > 3000)
                    gameStarted = true;
                compteurDebutDePartie += gameTime.ElapsedGameTime.Milliseconds;
            }
        }
        public override void Render()
        {
            if (state == SceneState.Active)
            {
                TurkeySmashGame.spriteBatch.Begin();

                level.Draw(TurkeySmashGame.spriteBatch);
                hud.Draw();
                if (compteur != null)
                    compteur.Draw(TurkeySmashGame.spriteBatch);

                TurkeySmashGame.spriteBatch.End();
            }
        }
    }
}
