using System.Drawing;

ForegroundColor = ConsoleColor.Cyan;
Write("Enter Directory path: ");
ForegroundColor = ConsoleColor.DarkGray;
var dir = ReadLine();
if (!Directory.Exists(dir)) 
{
    ForegroundColor = ConsoleColor.Red;
    WriteLine("Directory not found");
    return;
}

ForegroundColor = ConsoleColor.Magenta;
WriteLine("Analyzing...");
var x = Directory.EnumerateDirectories(dir);
long dirSize, fileCount, dirCount = 0;
EnumerateDirectory(dir, ref dirSize);

var fc = ForegroundColor;
ForegroundColor = ConsoleColor.DarkGray;
WriteLine("--------------------------------------------------------------");
WriteLine($"Dir: {dir}");
WriteLine($"Size: {dirSize / 1024 * 1024:N0} MB ({dirSize:N0}) bytes)");
WriteLine($"Contains: {dirCount:N0} folders; {fileCount:N0} files");
WriteLine("--------------------------------------------------------------");
ForegroundColor = fc;

void EnumerateDirectory(string directory, ref long dirSize)
{
    if (!Directory.Exists(directory)) return;

    foreach (var item in Directory.EnumerateFileSystemEntries(directory))
    {
        long tempDirSize = 0;
        if (Directory.Exists(item))
        {
            dirCount++;
            EnumerateDirectory(item, ref tempDirSize);
            //WriteLine($"Dir: {item} - Size: {tempDirSize / 1024 * 1024:N0} MB");
        }
        else
        {
            fileCount++;
            if (!File.Exists(item)) continue;
            var fi = new FileInfo(item);
            tempDirSize+= fi.Length;
        }
        dirSize += tempDirSize;
    }
}
