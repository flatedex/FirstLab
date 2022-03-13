﻿using System;
using System.Collections.Generic;
using System.IO;

namespace FirstLab
{
    public class FileWork
    {
        public static Boolean GetFromFile(Tree tree, List<Int32> vs)
        {
            String path = GetPath();
            String line;
            int value;
            try
            {
                using (StreamReader stream = new StreamReader(path))
                {
                    while ((line = stream.ReadLine()) != null)
                    {
                        try
                        {
                            value = Convert.ToInt32(line);
                            vs.Add(value);
                            tree.InsertNode(value);
                        }
                        catch
                        {
                            Console.WriteLine("Data is not correct, check file");
                            return false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Enter a valid filepath");
                return false;
            }
            return true;
        }
        public static String GetPath()
        {
            Console.WriteLine("Enter path to file:");
            String path = Console.ReadLine();
            try
            {
                path = Path.GetFullPath(path);
            }
            catch
            {
                Console.WriteLine("Check path to file");
                path = GetPath();
            }
            return path;
        }
        public static String CreateFile()
        {
            String path = GetPath();
            FileStream fStream = null;
            try
            {
                fStream = new FileStream(path, FileMode.CreateNew);
            }
            catch
            {
                if (File.Exists(path))
                {
                    Boolean rewrite = Menu.AskForRewriting();
                    if (rewrite)
                    {
                        File.Delete(path);
                        fStream = new FileStream(path, FileMode.CreateNew);
                    }
                    else
                    {
                        CreateFile();
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect file or invalid file name");
                    CreateFile();
                }
            }
            fStream.Close();
            return path;
        }
        public static void SaveInput(List<Int32> list)
        {
            String path = CreateFile();
            File.WriteAllLines(path, list.ConvertAll(x => x.ToString()));
        }
        public static void SaveResult(Node root)
        {
            String path = CreateFile();
            List<Int32> toFile = new List<Int32>();
            PreOrder(root, path, toFile);
            String text = "";
            foreach (Int32 item in toFile)
            {
                text += item.ToString() + " ";
            }
            File.WriteAllText(path, "Breadth-first output: " + text);
        }
        public static void PreOrder(Node root, String path, List<Int32> toFile) //breadth-first output to file
        {
            if (root != null)
            {
                toFile.Add(root.key);
                PreOrder(root.left, path, toFile);
                PreOrder(root.right, path, toFile);
            }
        }
    }
}