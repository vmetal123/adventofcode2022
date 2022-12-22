namespace adventofcode2022;

public class DaySeven
{
    public static async Task<long> Part1()
    {
        var lines = await ReadAndParseFile();

        var rootDirectory = ParseInstructionsToTree(lines);

        var smallest = new List<Directory>();

        Traverse(100000, rootDirectory, smallest);

        return smallest.Sum(x => x.TotalSize);
    }

    public static async Task<long> Part2()
    {
        var lines = await ReadAndParseFile();

        var rootDirectory = ParseInstructionsToTree(lines);

        var totalAvailableSpace = 70_000_000;
        var requiredSpace = 30_000_000;

        var unusedSpace = totalAvailableSpace - rootDirectory.TotalSize;
        var neededSpace = requiredSpace - unusedSpace;

        var bestDirectory = rootDirectory;
        GetBestSpace(neededSpace, rootDirectory, ref bestDirectory);

        return bestDirectory.TotalSize;
    }

    static void GetBestSpace(long neededSpace, Directory source, ref Directory bestDirectory)
    {
        if (source.TotalSize >= neededSpace && source.TotalSize < bestDirectory.TotalSize)
            bestDirectory = source;

        foreach (var directory in source.Directories)
        {
            GetBestSpace(neededSpace, directory, ref bestDirectory);
        }
    }

    static Directory ParseInstructionsToTree(string[] lines)
    {
        string CD = "cd";
        string OUT = "..";
        string START = "$";
        string DIR = "dir";
        string ROOT = "/";
        var rootDirectory = new Directory { Name = ROOT };
        Directory? currentDirectory = new Directory();

        foreach (var line in lines)
        {
            var instruction = line.Split(" ");

            if (instruction[0] == START)
            {
                if (instruction[1] == CD)
                {
                    if (instruction[2] == ROOT)
                    {
                        currentDirectory = rootDirectory;
                    }
                    else if (instruction[2] == OUT)
                    {
                        currentDirectory = currentDirectory?.Parent;
                    }
                    else
                    {
                        currentDirectory = currentDirectory?.Directories.Where(x => x.Name == instruction[2]).FirstOrDefault();
                    }
                }
            }

            if (instruction[0] == DIR)
            {
                currentDirectory!.Directories.Add(new Directory { Name = instruction[1], Parent = currentDirectory });
            }

            if (long.TryParse(instruction[0], out long size))
            {
                var file = new FileInfo(instruction[1], size);
                currentDirectory!.Files.Add(file);
            }
        }

        return rootDirectory;
    }

    static void Traverse(long maxSize, Directory source, List<Directory> smallest)
    {
        if (source.TotalSize <= maxSize)
            smallest.Add(source);

        foreach (var directory in source.Directories)
        {
            Traverse(maxSize, directory, smallest);
        }
    }

    static async Task<string[]> ReadAndParseFile()
        => await File.ReadAllLinesAsync("day7.txt");
}

class Directory
{
    public string Name { get; set; } = default!;
    public Directory Parent { get; set; } = default!;
    public List<Directory> Directories { get; set; } = new List<Directory>();
    public List<FileInfo> Files { get; set; } = new List<FileInfo>();
    private long? _TotalSize { get; set; }

    public long TotalSize
    {
        get
        {
            if (_TotalSize.HasValue)
            {
                return _TotalSize.Value;
            }

            _TotalSize = Directories.Sum(x => x.TotalSize) + Files.Sum(x => x.size);

            return _TotalSize.Value;
        }
    }
}

record FileInfo(string name, long size);