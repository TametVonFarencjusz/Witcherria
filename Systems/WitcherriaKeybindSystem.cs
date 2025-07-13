using Terraria.ModLoader;

namespace Witcherria.Systems
{
    // Acts as a container for keybinds registered by this mod.
    // See Common/Players/ExampleKeybindPlayer for usage.
    public class WitcherriaKeybindSystem : ModSystem
    {
        public static ModKeybind ShowSignsKeybind { get; private set; }

        public override void Load()
        {
            // Registers a new keybind
            // We localize keybinds by adding a Mods.{ModName}.Keybind.{KeybindName} entry to our localization files. The actual text displayed to english users is in en-US.hjson
            ShowSignsKeybind = KeybindLoader.RegisterKeybind(Mod, "Show Signs", "X");
        }

        // Please see ExampleMod.cs' Unload() method for a detailed explanation of the unloading process.
        public override void Unload()
        {
            // Not required if your AssemblyLoadContext is unloading properly, but nulling out static fields can help you figure out what's keeping it loaded.
            ShowSignsKeybind = null;
        }
    }
}
