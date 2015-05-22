using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace FileMonitor
{
    static class Program
    {
        // FileSystemWatcher
        private static FileSystemWatcher fsWatcher;
        private static System.Timers.Timer changeNotifier = new System.Timers.Timer(1000);
        private static List<string> createList = new List<string>();
        private static List<string> deleteList = new List<string>();
        private static List<string> renameList = new List<string>();
        private static List<string> changeList = new List<string>();
        private static object watcherLock = new object();
        private static object listLock = new object();

        // Tree data structure
        static Dictionary<string, UInt32> createCount = new Dictionary<string, UInt32>();
        static Dictionary<string, UInt32> deleteCount = new Dictionary<string, UInt32>();
        static Dictionary<string, UInt32> renameCount = new Dictionary<string, UInt32>();
        static Dictionary<string, UInt32> changeCount = new Dictionary<string, UInt32>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void CreateFSWatcher(string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                // sub watcher
                fsWatcher = new FileSystemWatcher();
                fsWatcher.Path = path;
                fsWatcher.IncludeSubdirectories = true;
                fsWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.Attributes | NotifyFilters.LastAccess | NotifyFilters.Security;
                // Add event handlers.
                fsWatcher.Created += new FileSystemEventHandler(handleCreateEvent);
                fsWatcher.Deleted += new FileSystemEventHandler(handleDeleteEvent);
                fsWatcher.Renamed += new RenamedEventHandler(handleRenameEvent);
                fsWatcher.Changed += new FileSystemEventHandler(handleChangeEvent);
                //Begin watching
                fsWatcher.EnableRaisingEvents = true;

                changeNotifier = new System.Timers.Timer(1000);
                changeNotifier.Elapsed += new ElapsedEventHandler(handleFileSystemChange);
                changeNotifier.Start();
            }
        }

        public static void StopFSWatcher()
        {
            fsWatcher.EnableRaisingEvents = false;
            fsWatcher.Dispose();
        }

        public static Dictionary<string, UInt32> GetFullList()
        {
            Dictionary<string, UInt32> fullList = new Dictionary<string, UInt32>();

            changeNotifier.Enabled = false;

            lock (listLock)
            {
                // Combine all the dictionaries into one
                foreach (KeyValuePair<string, UInt32> pair in changeCount)
                {
                    if (fullList.ContainsKey(pair.Key))
                    {
                        // Add to existing value
                        fullList[pair.Key] = fullList[pair.Key] + pair.Value;
                    }
                    else
                    {
                        // First time we've seen it!
                        fullList[pair.Key] = pair.Value;
                    }
                }

                foreach (KeyValuePair<string, UInt32> pair in createCount)
                {
                    if (fullList.ContainsKey(pair.Key))
                    {
                        // Add to existing value
                        fullList[pair.Key] = fullList[pair.Key] + pair.Value;
                    }
                    else
                    {
                        // First time we've seen it!
                        fullList[pair.Key] = pair.Value;
                    }
                }

                foreach (KeyValuePair<string, UInt32> pair in deleteCount)
                {
                    if (fullList.ContainsKey(pair.Key))
                    {
                        // Add to existing value
                        fullList[pair.Key] = fullList[pair.Key] + pair.Value;
                    }
                    else
                    {
                        // First time we've seen it!
                        fullList[pair.Key] = pair.Value;
                    }
                }

                foreach (KeyValuePair<string, UInt32> pair in renameCount)
                {
                    if (fullList.ContainsKey(pair.Key))
                    {
                        // Add to existing value
                        fullList[pair.Key] = fullList[pair.Key] + pair.Value;
                    }
                    else
                    {
                        // First time we've seen it!
                        fullList[pair.Key] = pair.Value;
                    }
                }
            }

            changeNotifier.Enabled = true;

            return fullList;
        }

        private static void handleFileSystemChange(Object sender, ElapsedEventArgs args)
        {
            lock (watcherLock)
            {
                changeNotifier.Enabled = false;
                processChanges();
                changeNotifier.Enabled = true;
            }
        }

        private static void processChanges()
        {
            try
            {
                lock (listLock)
                {
                    while (true)
                    {
                        if (createList.Count > 0)
                        {
                            string created = createList[0];
                            if (createCount.ContainsKey(created))
                            {
                                // Add one to the counter
                                createCount[created] = createCount[created] + 1;
                            }
                            else
                            {
                                // New, so add to the list!
                                createCount.Add(created, 1);
                            }
                            createList.RemoveAt(0);
                        }
                        else if (deleteList.Count > 0)
                        {
                            string deleted = deleteList[0];
                            if (deleteCount.ContainsKey(deleted))
                            {
                                // Add one to the counter
                                deleteCount[deleted] = deleteCount[deleted] + 1;
                            }
                            else
                            {
                                // New, so add to the list!
                                deleteCount.Add(deleted, 1);
                            }
                            deleteList.RemoveAt(0);
                        }
                        else if (renameList.Count > 0)
                        {
                            string renamed = renameList[0];
                            if (renameCount.ContainsKey(renamed))
                            {
                                // Add one to the counter
                                renameCount[renamed] = renameCount[renamed] + 1;
                            }
                            else
                            {
                                // New, so add to the list!
                                renameCount.Add(renamed, 1);
                            }
                            renameList.RemoveAt(0);
                        }
                        else if (changeList.Count > 0)
                        {
                            string changed = changeList[0];
                            if (changeCount.ContainsKey(changed))
                            {
                                // Add one to the counter
                                changeCount[changed] = changeCount[changed] + 1;
                            }
                            else
                            {
                                // New, so add to the list!
                                changeCount.Add(changed, 1);
                            }
                            changeList.RemoveAt(0);
                        }
                        else
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("FSWatcher processChanges Exception: " + ex.ToString());
                createList.Clear();
                deleteList.Clear();
                renameList.Clear();
                changeList.Clear();
            }
        }

        private static void handleRenameEvent(Object sender, RenamedEventArgs args)
        {
            lock (watcherLock)
            {
                changeNotifier.Enabled = false;
                renameList.Add(args.FullPath);
                changeNotifier.Enabled = true;
            }
        }

        private static void handleCreateEvent(Object sender, FileSystemEventArgs args)
        {
            lock (watcherLock)
            {
                changeNotifier.Enabled = false;
                createList.Add(args.FullPath);
                changeNotifier.Enabled = true;
            }
        }

        private static void handleDeleteEvent(Object sender, FileSystemEventArgs args)
        {
            lock (watcherLock)
            {
                changeNotifier.Enabled = false;
                deleteList.Add(args.FullPath);
                changeNotifier.Enabled = true;
            }
        }

        private static void handleChangeEvent(Object sender, FileSystemEventArgs args)
        {
            lock (watcherLock)
            {
                changeNotifier.Enabled = false;
                changeList.Add(args.FullPath);
                changeNotifier.Enabled = true;
            }
        }
    }
}
