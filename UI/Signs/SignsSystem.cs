using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Witcherria.UI.Signs
{
    [Autoload(Side = ModSide.Client)] // This attribute makes this class only load on a particular side. Naturally this makes sense here since UI should only be a thing clientside. Be wary though that accessing this class serverside will error
    public class SignsSystem : ModSystem
    {
        private UserInterface signsUserInterface;
        internal SignsState signsUI;

        // These two methods will set the state of our custom UI, causing it to show or hide
        public void ShowMyUI()
        {
            signsUserInterface?.SetState(signsUI);
        }

        public void HideMyUI()
        {
            signsUserInterface?.SetState(null);
        }

        public override void Load()
        {
            // Create custom interface which can swap between different UIStates
            signsUserInterface = new UserInterface();
            // Creating custom UIState
            signsUI = new SignsState();

            // Activate calls Initialize() on the UIState if not initialized, then calls OnActivate and then calls Activate on every child element
            signsUI.Activate();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            // Here we call .Update on our custom UI and propagate it to its state and underlying elements
            if (signsUserInterface?.CurrentState != null)
            {
                signsUserInterface?.Update(gameTime);
            }
        }

        // Adding a custom layer to the vanilla layer list that will call .Draw on your interface if it has a state
        // Setting the InterfaceScaleType to UI for appropriate UI scaling
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Witcheria: Choose Sign",
                    delegate
                    {
                        if (signsUserInterface?.CurrentState != null)
                        {
                            signsUserInterface.Draw(Main.spriteBatch, new GameTime());
                        }

                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
