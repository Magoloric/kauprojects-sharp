using System;
using System.IO;
using System.Xml;


namespace MediaIntegrator
{
    class ConvertController
    {
        public int CSVtoXML()
        {
            try
            {
                //Hard-coded path, according to assignment
                string[] data = File.ReadAllLines("frMediaShop/data.txt");
                if (File.Exists("TillSimpleMedia/data.xml"))
                {
                    //If a file exists, delete it to replace with a new one later
                    File.Delete("TillSimpleMedia/data.xml");
                }
                try
                {
                    //Create XML document.
                    XmlWriter output = XmlWriter.Create("TillSimpleMedia/data.xml");
                    output.WriteStartDocument(); //Start writing
                    output.WriteStartElement("Inventory"); //Write general element
                    for (int i = 0; i < data.Length; i++) //This goes on for all the items in storage.
                    {
                        string[] values = data[i].Split(';'); //split the data string
                        output.WriteStartElement("Item"); //Start each element
                        output.WriteStartElement("Name"); //Since order of elements is static in both XML and CSV, write values and elements in same order as sample XML shows
                        output.WriteString(values[7]); 
                        output.WriteEndElement();
                        output.WriteStartElement("Count");
                        output.WriteString(values[4]);
                        output.WriteEndElement();
                        output.WriteStartElement("Price");
                        output.WriteString(values[3]);
                        output.WriteEndElement();
                        output.WriteStartElement("Comment");
                        //Comment depends on product type (done because target software does not support product types
                        if (values[0] == "Book")
                        {
                            output.WriteString("Produkttyp: Bok \n Produktnamn: " + values[2] + "\n ISBN: " + values[12] + "\n Antal sidor: " + values[13] + "\n");
                        }
                        else if (values[0] == "Music")
                        {
                            output.WriteString("Produkttyp: Musikskiva \n Produktnamn: " + values[2] + "\n Längd: " + values[12] + "\n Antal spår: " + values[13] + "\n");
                        }
                        else if (values[0] == "Movie")
                        {
                            output.WriteString("Produkttyp: Film \n Produktnamn: " + values[2] + "\n Antal spår: " + values[12] + "\n");
                        }
                        else if (values[0] == "Game")
                        {
                            output.WriteString("Produkttyp: Spel \n Produktnamn: " + values[2] + "\n Platform: " + values[12] + "\n");
                        }
                        //Just in case
                        else
                        {
                            output.WriteString("");
                        }
                        output.WriteEndElement();
                        output.WriteStartElement("Artist");
                        output.WriteString(values[11]);
                        output.WriteEndElement();
                        output.WriteStartElement("Publisher");
                        output.WriteString(values[10]);
                        output.WriteEndElement();
                        output.WriteStartElement("Genre");
                        output.WriteString(values[8]);
                        output.WriteEndElement();
                        output.WriteStartElement("Year");
                        output.WriteString(values[9]);
                        output.WriteEndElement();
                        output.WriteStartElement("ProductID");
                        output.WriteString(values[1]);
                        output.WriteEndElement();
                        output.WriteEndElement(); //End of item data
                    }
                    output.WriteEndElement(); //End of inventory data
                    output.WriteEndDocument(); //End writing process
                    output.Close(); //Close document
                    return 0;
                }
                catch
                {
                    //If something goes wrong during XML writing
                    return 2;
                }
            }
            catch //If something goes wrong during file operations
            {
                return 1;
            }
        }
        public int XMLtoCSV()
        {
            try //If something goes wrong during conversion process
            {
                XmlReader input;
                try
                {
                    input = XmlReader.Create("frSimpleMedia/data.xml"); //Read XML file
                }
                catch //if something goes wrong during XML read (if file does not exist or such)
                {
                    return 2; 
                }
                int i = -1; //for incremention
                string[] data = new string[0]; //for CSV data
                //Property variables (Since order of properties are different in XML than in CSV.
                string type = "", id = "", name = "", price = "", stock = "", title = "", genre = "", year = "", publisher = "", creator = "", extra1 = "", extra2 = "";
                while (input.Read()) //Until end of XML file is reached
                {
                    if ((input.NodeType == XmlNodeType.Element) && (input.Name == "Item")) //If there's an item upcoming, increment and resize the data array 
                                                                                           //Needed in order to avoid extra empty cells.
                    {
                        i++;
                        Array.Resize(ref data, data.Length + 1);
                    }
                    //Find elements with property names and get text values from them.
                    if ((input.NodeType == XmlNodeType.Element) && (input.Name == "Name"))
                    {
                        title = input.ReadElementContentAsString();
                    }
                    if ((input.NodeType == XmlNodeType.Element) && (input.Name == "Count"))
                    {
                        stock = input.ReadElementContentAsString();
                    }
                    if ((input.NodeType == XmlNodeType.Element) && (input.Name == "Price"))
                    {
                        price = input.ReadElementContentAsString();
                    }
                    if ((input.NodeType == XmlNodeType.Element) && (input.Name == "Artist"))
                    {
                        creator = input.ReadElementContentAsString();
                    }
                    if ((input.NodeType == XmlNodeType.Element) && (input.Name == "Comment"))
                    {
                        //If comment contains valuable information for Shopstock, try and resolve it
                        string comment = input.ReadElementContentAsString();
                        if (comment.Contains("Produkttyp: Bok"))
                        {
                            type = "Book";
                            if (comment.Contains("Produktnamn: "))
                            {
                                name = comment.Substring(comment.IndexOf("Produktnamn: ") + 13, comment.IndexOf("\n", comment.IndexOf("Produktnamn: ") + 13) - (comment.IndexOf("Produktnamn: ") + 13));
                            }
                            else
                            {
                                name = "";
                            }
                            if (comment.Contains("ISBN: "))
                            {
                                extra1 = comment.Substring(comment.IndexOf("ISBN: ") + 6, comment.IndexOf("\n", comment.IndexOf("ISBN: ") + 6) - (comment.IndexOf("ISBN: ") + 6)) + ";";
                            }
                            else
                            {
                                extra1 = "Okänd;";
                            }
                            if (comment.Contains("Antal sidor: "))
                            {
                                extra2 = comment.Substring(comment.IndexOf("Antal sidor: ") + 13, comment.IndexOf("\n", comment.IndexOf("Antal sidor: ") + 13) - (comment.IndexOf("Antal sidor: ") + 13));
                            }
                            else
                            {
                                extra2 = "0";
                            }
                        }
                        else if (comment.Contains("Produkttyp: Musikskiva"))
                        {
                            type = "Music";
                            if (comment.Contains("Produktnamn: "))
                            {
                                name = comment.Substring(comment.IndexOf("Produktnamn: ") + 13, comment.IndexOf("\n", comment.IndexOf("Produktnamn: ") + 13) - (comment.IndexOf("Produktnamn: ") + 13));
                            }
                            else
                            {
                                name = "";
                            }
                            if (comment.Contains("Längd: "))
                            {
                                extra1 = comment.Substring(comment.IndexOf("Längd: ") + 7, comment.IndexOf("\n", comment.IndexOf("Längd: ") + 7) - (comment.IndexOf("Längd: ") + 7)) + ";";
                            }
                            else
                            {
                                extra1 = "Okänd;";
                            }
                            if (comment.Contains("Antal spår: "))
                            {
                                extra2 = comment.Substring(comment.IndexOf("Antal spår: ") + 12, comment.IndexOf("\n", comment.IndexOf("Antal spår: ") + 12) - (comment.IndexOf("Antal spår: ") + 12));
                            }
                            else
                            {
                                extra2 = "0";
                            }
                        }
                        else if (comment.Contains("Produkttyp: Film"))
                        {
                            type = "Movie";
                            extra2 = "";
                            if (comment.Contains("Produktnamn: "))
                            {
                                name = comment.Substring(comment.IndexOf("Produktnamn: ") + 13, comment.IndexOf("\n", comment.IndexOf("Produktnamn: ") + 13) - (comment.IndexOf("Produktnamn: ") + 13));
                            }
                            else
                            {
                                name = "";
                            }
                            if (comment.Contains("Antal episoder: "))
                            {
                                extra1 = comment.Substring(comment.IndexOf("Antal episoder: ") + 16, comment.IndexOf("\n", comment.IndexOf("Antal episoder: ") + 16) - (comment.IndexOf("Antal episoder: ") + 16));
                            }
                            else
                            {
                                extra1 = "0";
                            }
                        }
                        else if (comment.Contains("Produkttyp: Spel"))
                        {
                            type = "Game";
                            extra2 = "";
                            if (comment.Contains("Produktnamn: "))
                            {
                                name = comment.Substring(comment.IndexOf("Produktnamn: ") + 13, comment.IndexOf("\n", comment.IndexOf("Produktnamn: ") + 13) - (comment.IndexOf("Produktnamn: ") + 13));
                            }
                            else
                            {
                                name = "";
                            }
                            if (comment.Contains("Platform: "))
                            {
                                extra1 = comment.Substring(comment.IndexOf("Platform: ") + 10, comment.IndexOf("\n", comment.IndexOf("Platform: ") + 10) - (comment.IndexOf("Platform: ") + 10));
                            }
                            else
                            {
                                extra1 = "Okänd";
                            }
                        }
                        else //If no info exist, put static type to the item (since all sample items were music)
                        {
                            name = "";
                            type = "Music";
                            extra1 = "0;";
                            extra2 = "0";
                        }
                    }
                    if ((input.NodeType == XmlNodeType.Element) && (input.Name == "Publisher"))
                    {
                        publisher = input.ReadElementContentAsString();
                    }
                    if ((input.NodeType == XmlNodeType.Element) && (input.Name == "Genre"))
                    {
                        genre = input.ReadElementContentAsString();
                    }
                    if ((input.NodeType == XmlNodeType.Element) && (input.Name == "Year"))
                    {
                        year = input.ReadElementContentAsString();
                    }
                    if ((input.NodeType == XmlNodeType.Element) && (input.Name == "ProductID"))
                    {
                        id = input.ReadElementContentAsString();
                    }
                    if ((input.NodeType == XmlNodeType.EndElement) && (input.Name == "Item")) //When element ends
                    {
                        if (name == "") //If name wasn't read from comment, create it
                        {
                            name = creator + " - " + title;
                        }
                        //Write a string containing item's data separated with ;
                        data[i] = type + ";" + id + ";" + name + ";" + price + ";" + stock + ";" + 0 + ";" + 0 + ";" + title + ";" + genre + ";" + year + ";" + publisher + ";" + creator + ";" + extra1 + extra2;
                    }
                }
                //If file exists, delete it to write a new one later
                if (File.Exists("tillMediaShop/data.txt"))
                {
                    File.Delete("tillMediaShop/data.txt");
                }
                File.WriteAllLines("tillMediaShop/data.txt", data);
                return 0;

            }
            catch 
            {
                return 1;
            }
        }
    }
}
