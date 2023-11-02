using System;
using System.Linq;
using Microsoft.Extensions.Options;
using MyGen.Data;

namespace Genealogy.Console.MyGenImport;

public class FileSystem : IFileSystem
{
    private readonly string _path;

    public FileSystem()
    {
        _path = "C:\\Users\\marti\\source\\repos\\my-gen-data";
    }

    public void DeleteFile(string filename)
    {
        File.Delete(Path.Combine(_path, filename));
    }

    public ICollection<string> GetFiles()
    {
        return Directory.GetFiles(_path);
    }

    public ICollection<string> GetFiles(string extension)
    {
        return Directory.GetFiles(_path, $"*{extension}");
    }

    public Stream OpenForRead(string filename)
    {
        return new FileStream(Path.Combine(_path, filename), FileMode.Open);
    }

    public Stream OpenForWrite(string filename)
    {
        return new FileStream(Path.Combine(_path, filename), FileMode.Create);
    }
}