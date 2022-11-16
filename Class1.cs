using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CSVtoJSONLib
{
    public class Processor
    {
        public static (int,int) ProcessLines(String dirPath)
        {
            //for storing unique transcation id's
            HashSet<String> validTransactions = new HashSet<String>();
            //for storing unique valid accounts
            HashSet<String> validAccounts = new HashSet<String>();

            try
            {
                if (File.Exists(dirPath))
                {
                    Console.WriteLine("File is Exist");
                    String[] lines = File.ReadAllLines(dirPath);
                    for (int i = 1; i < lines.Length; i++)
                    {
                        String[] arr = lines[i].Split(',');
                        if (arr[0] == "" || arr[1] == "" || arr[2] == "" || arr[3] == "" || arr[4] == "" || arr[5] == "")
                        {
                            continue;
                        }
                        else
                        {
                            if (validTransactions.Contains(arr[0]))
                            {
                                continue;
                            }
                            else
                            {
                                validTransactions.Add(arr[0]);
                                Modal model = new Modal();
                                model.AccountID = arr[3];
                                model.AccountOwnerName = arr[4];
                                model.AccountType = arr[5];
                                model.TransactionID = Convert.ToInt32(arr[0]);
                                model.TransactionDate = arr[1];
                                model.TransactionAmout = Convert.ToDecimal(arr[2]);


                                string result = JsonConvert.SerializeObject(model, Formatting.Indented);
                                //creating an storing the output in the Json file
                                int count = 0;
                                string filepath = @"D:\inputCSV";
                                string extension = ".json";
                                string extensionRes = filepath+extension;
                                if (File.Exists(filepath + extension))
                                {
                                    count++;
                                    String incCount = filepath + count;
                                    File.AppendAllText(incCount+extension, result);
                                }
                                else
                                {
                                    File.AppendAllText(filepath + extension, result);
                                }

                                Console.WriteLine(result);
                            }
                            //Because 1 account can have multiple transactions
                            if (validAccounts.Contains(arr[3]))
                            {
                                continue;
                            }
                            else
                            {
                                validAccounts.Add(arr[3]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("File is not Found at specified location");
            }
            return ((validTransactions.Count()), (validAccounts.Count()));
            Console.ReadLine();
        }
        public static void Main(String[] args)
        {
            Console.WriteLine("Enter the Path of csv file: ");
            var dirPath = @"" + Console.ReadLine();
            Console.WriteLine(ProcessLines(dirPath));
            Console.WriteLine("You have Successfully got your counts!!.");
            Console.ReadLine();
        }
    }
}
public class Modal
{
    public string AccountID;
    public string AccountOwnerName;
    public string AccountType;
    public int TransactionID;
    public string TransactionDate;
    public decimal TransactionAmout;
}