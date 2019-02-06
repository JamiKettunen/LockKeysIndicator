using System;
using IWshRuntimeLibrary;

public class Shortcut
{
    private WshShell shell = new WshShell();

    public String Address = ""; // Shortcut saving address
    public String Target = ""; // Targeted file
    public String Arguments = ""; // Target file arguments
    public String Description = ""; // This is an example description.
    public String Hotkey = ""; // CTRL+ALT+BACK etc.
    public String IconLocation = "";
    public String RelativePath = "";
    public Int32 WindowStyle = -1;
    public String WorkingDir = "";

    public Boolean Create()
    {
        try
        {
            /*if (!System.IO.File.Exists(this.Address)) {  } else {  }*/

            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(this.Address);

            shortcut.Description = this.Description;
            shortcut.Arguments = this.Arguments;
            shortcut.Hotkey = this.Hotkey;
            if (this.IconLocation != "") { shortcut.IconLocation = this.IconLocation; }
            if (this.RelativePath != "") { shortcut.RelativePath = this.RelativePath; }
            if (this.Target != "") { shortcut.TargetPath = this.Target; }
            if (this.WindowStyle != -1) { shortcut.WindowStyle = this.WindowStyle; }
            if (this.WorkingDir != "") { shortcut.WorkingDirectory = this.WorkingDir; }

            shortcut.Save();

            return true;
        }
        catch { return false; }
    }
}
