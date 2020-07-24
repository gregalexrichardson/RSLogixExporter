using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Microsoft.VisualBasic;

namespace RSLogixEditor
{
    public partial class Form1 : Form
    {
        public String fileLoc;
        public XmlDocument xmlDocument;
        public Dictionary<String, Action<string, string, string, string, string, string>> functionDictionary = new Dictionary<string, Action<string, string, string, string, string, string>>();
        public Form1()
        {
            InitializeComponent();

            cmbLookIn.SelectedIndexChanged += UpdateCombos;

            functionDictionary.Add("Direct Replace", DirectReplace);
            functionDictionary.Add("Increment by Condition", Increment);
            functionDictionary.Add("Increment + Letter", IncrementWithText);
            functionDictionary.Add("Increment + Letter + Replace", IncrementWithTextAndReplace);
            functionDictionary.Add("Fix Comments", FixComments);
            functionDictionary.Add("Fix Alarms", fixAlarmTags);
            functionDictionary.Add("Fix Remote", fixRemote);

            foreach (KeyValuePair<String, Action<string, string, string, string, string, string>> de in functionDictionary)
            {
                cmbFunction.Items.Add(de.Key);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    fileLoc = ofd.FileName;
                    lblFilePath.Text = fileLoc;
                    OpenXML();
                }
                else
                {
                    lblFilePath.Text = "NO FILE SELECTED";
                    return;
                }
            }


        }

        private void OpenXML()
        {
            xmlDocument = new XmlDocument();
            xmlDocument.Load(fileLoc);
            cmbLookIn.Items.AddRange((from XmlNode xn in xmlDocument.SelectNodes("//*") select xn.Name).Distinct().ToArray());
        }

        private void UpdateCombos(object sender, System.EventArgs e)
        {
            XmlNode xmlNode = xmlDocument.SelectSingleNode("//"+cmbLookIn.SelectedItem.ToString());
            cmbFindFrom.Items.AddRange((from XmlNode xn in xmlNode.ChildNodes select xn.Name).Distinct().ToArray());
            cmbReplaceFrom.Items.AddRange((from XmlNode xn in xmlNode.ChildNodes select xn.Name).Distinct().ToArray());
        }


        public void DirectReplace(string lookIn, string findFrom, string findValue, string replaceFrom, string replaceValue, string conditions)
        {
            XmlNodeList lookNodes = xmlDocument.SelectNodes("//" + lookIn);
            foreach (XmlNode xn in lookNodes)
            {
                XmlNode findNode = xn.SelectSingleNode(findFrom);
                XmlNode replaceNode = xn.SelectSingleNode(replaceFrom);

                if (findNode == null)
                    continue;
                if (replaceNode == null)
                    continue;

                String findSearchLeft = findValue.Substring(0, findValue.IndexOf("###"));
                String findSearchRight = findValue.Substring(findValue.IndexOf("###") + 3, findValue.Length - findValue.IndexOf("###") - 3);

                string findInner = findNode.InnerXml;
                String findExtract;
                int findIn1;
                int findIn2;
                findIn1 = findInner.IndexOf(findSearchLeft, 0);
                if (findIn1 == -1)
                    continue;
                findIn2 = findInner.IndexOf(findSearchRight, findIn1 + findSearchLeft.Length);
                if (findIn2 == -1)
                    continue;
                findExtract = findInner.Substring(findIn1, findIn2 - findIn1 + 1);
                String findNumberString = (findExtract.Replace(findSearchLeft, "").Replace(findSearchRight, ""));
                int findNumber = int.Parse(findNumberString);

                                
                String replaceSearchLeft = replaceValue.Substring(0, replaceValue.IndexOf("###"));
                String replaceSearchRight = replaceValue.Substring(replaceValue.IndexOf("###") + 3, replaceValue.Length - replaceValue.IndexOf("###") - 3);

                string replaceInner = replaceNode.InnerXml;
                String replaceExtract;
                int replaceIn1;
                int replaceIn2;
                replaceIn1 = replaceInner.IndexOf(replaceSearchLeft, 0);
                if (replaceIn1 == -1)
                    continue;
                replaceIn2 = replaceInner.IndexOf(replaceSearchRight, replaceIn1 + replaceSearchLeft.Length);
                if (replaceIn2 == -1)
                    continue;
                replaceExtract = replaceInner.Substring(replaceIn1, replaceIn2 - replaceIn1 + 1);
                String replaceNumberString = (replaceExtract.Replace(replaceSearchLeft, "").Replace(replaceSearchRight, ""));
                int replaceNumber = int.Parse(replaceNumberString);


                string findExtract2 = findExtract.Replace(findNumberString, replaceNumber.ToString());
                findNode.InnerXml = findNode.InnerXml.Replace(findExtract, findExtract2);
                replaceNode.InnerXml = replaceNode.InnerXml.Replace(findExtract, findExtract2);
                //extract string
                //findNode.InnerText
                //Debug.WriteLine("");
            }
        }

        public void Increment(string lookIn, string findFrom, string findValue, string replaceFrom, string replaceValue, string conditions)
        {

            int increment = 1;
            if(conditions == "")
            {
                increment = 1;
            }
            else
            {
                try
                {
                    increment = int.Parse(conditions);
                }
                catch
                {
                    increment = 1;
                }
            }


            String findSearchLeft = findValue.Substring(0, findValue.IndexOf("###"));
            String findSearchRight = findValue.Substring(findValue.IndexOf("###") + 3, findValue.Length - findValue.IndexOf("###") - 3);

            XmlNodeList lookNodes = xmlDocument.SelectNodes("//" + lookIn);
            foreach (XmlNode xn in lookNodes)
            {
                XmlNode findNode = xn.SelectSingleNode(findFrom);

                if (findNode == null)
                    continue;


                string findInner = findNode.InnerXml;
                String findExtract;
                int findIn1;
                int findIn2;
                findIn1 = findInner.IndexOf(findSearchLeft, 0);
                if (findIn1 == -1)
                    continue;
                findIn2 = findInner.IndexOf(findSearchRight, findIn1 + findSearchLeft.Length);
                if (findIn2 == -1)
                    continue;
                findExtract = findInner.Substring(findIn1, findIn2 - findIn1 + 1);
                String findNumberString = (findExtract.Replace(findSearchLeft, "").Replace(findSearchRight, ""));
                int findNumber = int.Parse(findNumberString);


                string findExtract2 = findExtract.Replace(findNumberString, (findNumber+increment).ToString());
                findNode.InnerXml = findNode.InnerXml.Replace(findExtract, findExtract2);
                //extract string
                //findNode.InnerText
                //Debug.WriteLine("");
            }
        }

        public void IncrementWithText(string lookIn, string findFrom, string findValue, string replaceFrom, string replaceValue, string conditions)
        {


            if (!findValue.Contains("@@@"))
                return;

            int increment = 1;
            if (conditions == "")
            {
                increment = 1;
            }
            else
            {
                try
                {
                    increment = int.Parse(conditions);
                }
                catch
                {
                    increment = 1;
                }
            }


            XmlNodeList lookNodes = xmlDocument.SelectNodes("//" + lookIn);
            foreach (XmlNode xn in lookNodes)
            {
                XmlNode findNode = xn.SelectSingleNode(findFrom);

                if (findNode == null)
                    continue;

                for(int iAsc = 65; iAsc <= 90; iAsc++)
                {
                    String findValueNew = findValue.Replace("@@@", "" + Convert.ToChar(iAsc));
                    String findSearchLeft = findValueNew.Substring(0, findValueNew.IndexOf("###"));
                    String findSearchRight = findValueNew.Substring(findValueNew.IndexOf("###") + 3, findValueNew.Length - findValueNew.IndexOf("###") - 3);

                    string findInner = findNode.InnerXml;
                    String findExtract;
                    int findIn1;
                    int findIn2;
                    findIn1 = findInner.IndexOf(findSearchLeft, 0);
                    if (findIn1 == -1)
                        continue;
                    findIn2 = findInner.IndexOf(findSearchRight, findIn1 + findSearchLeft.Length);
                    if (findIn2 == -1)
                        continue;
                    findExtract = findInner.Substring(findIn1, findIn2 - findIn1 + 1);
                    String findNumberString = (findExtract.Replace(findSearchLeft, "").Replace(findSearchRight, ""));
                    int findNumber = int.Parse(findNumberString);


                    string findExtract2 = findExtract.Replace(findNumberString, (findNumber + increment).ToString());
                    findNode.InnerXml = findNode.InnerXml.Replace(findExtract, findExtract2);

                }
               


                //extract string
                //findNode.InnerText
                //Debug.WriteLine("");
            }
        }

        public void IncrementWithTextAndReplace(string lookIn, string findFrom, string findValue, string replaceFrom, string replaceValue, string conditions)
        {


            if (!findValue.Contains("@@@"))
                return;

            int increment = 1;
            if (conditions == "")
            {
                increment = 1;
            }
            else
            {
                try
                {
                    increment = int.Parse(conditions);
                }
                catch
                {
                    increment = 1;
                }
            }


            XmlNodeList lookNodes = xmlDocument.SelectNodes("//" + lookIn);
            foreach (XmlNode xn in lookNodes)
            {
                XmlNode findNode = xn.SelectSingleNode(findFrom);

                if (findNode == null)
                    continue;

                for (int iAsc = 65; iAsc <= 90; iAsc++)
                {
                    String findValueNew = findValue.Replace("@@@", "" + Convert.ToChar(iAsc));

                    String findSearchLeft = findValueNew.Substring(0, findValueNew.IndexOf("###"));
                    String findSearchRight = findValueNew.Substring(findValueNew.IndexOf("###") + 3, findValueNew.Length - findValueNew.IndexOf("###") - 3);

                    string findInner = findNode.InnerXml;
                    String findExtract;
                    int findIn1;
                    int findIn2;
                    findIn1 = findInner.IndexOf(findSearchLeft, 0);
                    if (findIn1 == -1)
                        continue;
                    findIn2 = findInner.IndexOf(findSearchRight, findIn1 + findSearchLeft.Length);
                    if (findIn2 == -1)
                        continue;
                    findExtract = findInner.Substring(findIn1, findIn2 - findIn1 + 1);
                    String findNumberString = (findExtract.Replace(findSearchLeft, "").Replace(findSearchRight, ""));
                    int findNumber = int.Parse(findNumberString);


                    String replaceValueNew = replaceValue.Replace("@@@", "" + Convert.ToChar(iAsc));
                    replaceValueNew = replaceValueNew.Replace("###", (findNumber + increment).ToString());


                    //string findExtract2 = findExtract.Replace(findNumberString, (findNumber + increment).ToString());
                    findNode.InnerXml = findNode.InnerXml.Replace(findExtract, replaceValueNew);

                }



                //extract string
                //findNode.InnerText
                //Debug.WriteLine("");
            }
        }


        public void FixComments(string lookIn, string findFrom, string findValue, string replaceFrom, string replaceValue, string conditions)
        {

            //doesn't take any arguments

            int changes = 0;

            //cycle through rungs
            XmlNodeList lookNodes = xmlDocument.SelectNodes("//Rung");
            foreach (XmlNode xn in lookNodes)
            {
                XmlNode findNode = xn.SelectSingleNode("Text");
                //find where XIO + XIC + OTE == 2
                int occur = 0;
                occur += Regex.Matches(findNode.InnerXml.ToString(), "XIO").Count;
                occur += Regex.Matches(findNode.InnerXml.ToString(), "XIC").Count;
                occur += Regex.Matches(findNode.InnerXml.ToString(), "OTE").Count;
                if (occur != 2)
                    continue;

                //extract XIO and OTE tags
                MatchCollection matches = Regex.Matches(findNode.InnerXml, @"(?<=XIC\(|XIO\(|OTE\()(.*?)(?=\))");

                //Check whether one of the tags contains 'local'
                bool skip = true;
                foreach(Match m in matches)
                {
                    if (m.Value.ToUpper().Contains("LOCAL"))
                    {
                        skip = false;
                    }
                }
                if (skip)
                    continue;


                //Check whether tags have descriptions
                string[] descriptions = new string[matches.Count];
                for(int ii = 0; ii<matches.Count; ii++)
                {
                    Match m = matches[ii];
                    descriptions[ii] = getTagDescription(m.Value);
                }

                int countStr = 0;
                string descFinal = "";
                for(int ii = 0; ii<descriptions.Count(); ii++)
                {
                    if (descriptions[ii].Equals(""))
                        continue;
                    else
                        countStr++;

                    descFinal = descriptions[ii];
                }

                if (countStr != 1)
                    continue;


                //If 1 has description, copy description to other
                //If both have descriptions, raise error, log to file or somethign
                //If neither have descriptions, do nothing

                foreach (Match m in matches)
                {
                    setTagDescription(m.Value, descFinal);
                    changes++;
                }

            }
            MessageBox.Show("Changes: " + changes);
        }

        public void fixAlarmTags(string lookIn, string findFrom, string findValue, string replaceFrom, string replaceValue, string conditions)
        {
            //find all rungs where a tag sets an alarm trigger

            //cycle through rungs
            XmlNodeList lookNodes = xmlDocument.SelectNodes("//Rung/Text");
            foreach (XmlNode xn in lookNodes)
            {
                //.Activ
                MatchCollection matches = Regex.Matches(xn.InnerXml, @"XIC\([_a-zA-Z0-9\[\]\.]+\)[ ]{0,1}OTE\([_a-zA-Z0-9\[\]\.]+.Actv\)");

                foreach(Match m in matches)
                {
                    String mXIC = Regex.Match(m.Value, @"(?<=XIC\()[_a-zA-Z0-9\[\]\.]+(?=\))").Value ;
                    String mOTE = Regex.Match(m.Value, @"(?<=OTE\()[_a-zA-Z0-9\[\]\.]+(?=.Actv\))").Value;

                    //make sure OTE tag is NpAlarm
                    if (!getTagType(mOTE).Equals("NpAlarm"))
                        return;
                    //<Tag Name="_JIB_WS_LAN" TagType="Base" DataType="NpAlarm" Constant="false" ExternalAccess="Read/Write">

                    //NpAlarm(_JIB_WS_LAN, JIB_WS_LAN_T, 1)
                    String alarmExtract = Regex.Match(xmlDocument.InnerXml, @"(?<=NpAlarm\(" + mOTE + @",)[ _a-zA-Z0-9\[\]\.]+(?=,)").Value.Trim();

                    foreach(XmlNode xn2 in lookNodes)
                    {
                        if (xn2 == xn)
                            continue;

                        MatchCollection matches2 = Regex.Matches(xn2.InnerXml, @"XIC\(" + mXIC + @"\).*OTE\([_a-zA-Z0-9\[\]\.]+\)");
                        xn2.InnerXml = xn2.InnerXml.Replace(mXIC, alarmExtract);

                    }

                    //do the stuff

                    //get alm value of 
                }

                //get tag datatype
            }
        }

        private void fixRemote(string lookIn, string findFrom, string findValue, string replaceFrom, string replaceValue, string conditions)
        {
            //REM,XIC(Test_MSG_Trigger[188]) ]OTE
            XmlNodeList lookNodes = xmlDocument.SelectNodes("//Rung/Comment");
            foreach (XmlNode xn in lookNodes)
            {

                xn.InnerXml = Regex.Replace(xn.InnerXml, @"REM.*OTE", "REMOTE");
                
                //get tag datatype
            }
        }

        private string getTagType(string tag)
        {
            XmlNodeList lookNodes = xmlDocument.SelectNodes("//Tag");
            XmlNode[] nodesFiltered = (from XmlNode xn in lookNodes where xn.Attributes["Name"].Value.Equals(tag) select xn).ToArray();

            if (nodesFiltered.Length == 0)
            {
                lookNodes = xmlDocument.SelectNodes("//LocalTag");
                nodesFiltered = (from XmlNode xn in lookNodes where xn.Attributes["Name"].Value.Equals(tag) select xn).ToArray();

                if (nodesFiltered.Length == 0)
                    return "";
            }

            return nodesFiltered[0].Attributes["DataType"].Value.ToString();
        }

        private string getTagDescription(string tag)
        {

            if (tag.ToUpper().Contains("LOCAL:"))
            {
                //extract address
                String addr = Regex.Match(tag, @"(?<=Local:)(.*?)(?=:)").Value;
                //String addr = Regex.Matches(findNode.InnerXml, @"(?<=XIC\(|XIO\(|OTE\()(.*?)(?=\))").;
                //String loc = "";
                String loc = Regex.Match(tag, @"(?<=:I).*").Value;

                // cycle through modules
                XmlNodeList modNodes = xmlDocument.SelectNodes("//Module");
                XmlNode modNodeFiltered = (from XmlNode xn in modNodes where xn.SelectSingleNode("Ports/Port").Attributes["Address"].Value.Equals(addr) select xn).FirstOrDefault();
                if (modNodeFiltered == null)
                {
                    Debug.WriteLine(tag + ": No Modules");
                    return "";
                }

                //XmlNode modCommentNode = modNodeFiltered.SelectSingleNode("//Communications/Connections/Connection/Comments/Comment");
                XmlNode modCommentNode = (from XmlNode xn in modNodeFiltered.SelectNodes("Communications/Connections/Connection/InputTag/Comments/Comment") where xn.Attributes["Operand"] != null && xn.Attributes["Operand"].Value.ToUpper().Equals(loc.ToUpper()) select xn).FirstOrDefault();
                if (modCommentNode == null)
                {
                    Debug.WriteLine(tag + ": No Comments");
                    return "";
                }
                else
                {
                    Debug.WriteLine(tag + ": " + modCommentNode.InnerXml);
                    return modCommentNode.InnerXml;
                }
                // find correct port
                // Local:[PORT]:I.Data.XXXX
                // Local:4:I.Data.8
            }


            //loop through tags to find where text = tag (or whatever)
            //return description
            XmlNodeList lookNodes = xmlDocument.SelectNodes("//Tag");
            XmlNode[] nodesFiltered = (from XmlNode xn in lookNodes where xn.Attributes["Name"].Value.Equals(tag) select xn).ToArray();

            //foreach (XmlNode xn in lookNodes)
            //{
            //    if (xn.Attributes["Name"].Value.Equals(tag)){
            //        Debug.WriteLine("test");
            //    }
            //}

            if (nodesFiltered.Length == 0)
                return "";
            XmlNodeList descriptionNodes = nodesFiltered[0].SelectNodes("Description");
            if(descriptionNodes.Count > 0)
            {
                return descriptionNodes.Item(0).InnerXml;
            }
            else
            {
                return "";
            }
            
        }

        private void setTagDescription(string tag, string description)
        {
            //loop through tags to find where text = tag (or whatever)
            //return description

            if (tag.ToUpper().Contains("LOCAL:"))
            {
                //extract address
                String addr = Regex.Match(tag, @"(?<=Local:)(.*?)(?=:)").Value;
                //String addr = Regex.Matches(findNode.InnerXml, @"(?<=XIC\(|XIO\(|OTE\()(.*?)(?=\))").;
                //String loc = "";
                String loc = Regex.Match(tag, @"(?<=:I).*").Value;

                // cycle through modules
                XmlNodeList modNodes = xmlDocument.SelectNodes("//Module");
                XmlNode modNodeFiltered = (from XmlNode xn in modNodes where xn.SelectSingleNode("Ports/Port").Attributes["Address"].Value.Equals(addr) select xn).FirstOrDefault();
                if (modNodeFiltered == null)
                {
                    Debug.WriteLine(tag + ": No Modules");
                    return;
                }

                //XmlNode modCommentNode = modNodeFiltered.SelectSingleNode("//Communications/Connections/Connection/Comments/Comment");
                XmlNode modCommentNode = (from XmlNode xn in modNodeFiltered.SelectNodes("Communications/Connections/Connection/InputTag/Comments/Comment") where xn.Attributes["Operand"] != null && xn.Attributes["Operand"].Value.ToUpper().Equals(loc.ToUpper()) select xn).FirstOrDefault();
                if (modCommentNode == null)
                {
                    Debug.WriteLine(tag + ": No Comments");
                    XmlNode newNode = xmlDocument.CreateElement("Comment");
                    XmlAttribute newAttribute = xmlDocument.CreateAttribute("Operand");
                    newAttribute.Value = loc.ToUpper();
                    newNode.Attributes.Append(newAttribute);
                    newNode.InnerXml = description;

                    if(modNodeFiltered.SelectNodes("Communications/Connections/Connection/InputTag/Comments").Count > 0)
                    {
                        modNodeFiltered.SelectNodes("Communications/Connections/Connection/InputTag/Comments")[0].AppendChild(newNode);
                    }
                    else
                    {
                        XmlNode newCommentsNode = xmlDocument.CreateElement("Comments");
                        if (modNodeFiltered.SelectNodes("Communications/Connections/Connection/InputTag").Count > 0)
                        {

                            if (modNodeFiltered.SelectNodes("Communications/Connections/Connection/InputTag")[0].ChildNodes.Count > 0)
                            {
                                modNodeFiltered.SelectNodes("Communications/Connections/Connection/InputTag")[0].InsertBefore(newCommentsNode, modNodeFiltered.SelectNodes("Communications/Connections/Connection/InputTag")[0].ChildNodes[0]);
                            }
                            else
                            {
                                modNodeFiltered.SelectNodes("Communications/Connections/Connection/InputTag/Comments")[0].AppendChild(newCommentsNode);
                            }

                            //modNodeFiltered.SelectNodes("Communications/Connections/Connection/InputTag")[0].AppendChild(newCommentsNode);
                            modNodeFiltered.SelectNodes("Communications/Connections/Connection/InputTag/Comments")[0].AppendChild(newNode);
                        }
                        else
                        {
                            Debug.WriteLine("Do I have to add another level?!");
                        }
                    }

                    return;
                }
                else
                {
                    modCommentNode.InnerXml = description;
                    return;
                }
                //return;
            // find correct port
                // Local:[PORT]:I.Data.XXXX
                // Local:4:I.Data.8
            }

            XmlNodeList lookNodes = xmlDocument.SelectNodes("//Tag");
            XmlNode[] nodesFiltered = (from XmlNode xn in lookNodes where xn.Attributes["Name"].Value.ToUpper().Equals(tag.ToUpper()) select xn).ToArray();
            if(nodesFiltered.Length == 0)
            {
                XmlNode newNode = xmlDocument.CreateElement("Description");
                newNode.InnerXml = description;
                //nodesFiltered[0].AppendChild(newNode);
                return;
            }
            XmlNodeList descriptionNodes = nodesFiltered[0].SelectNodes("Description");
            if (descriptionNodes.Count > 0)
            {
                descriptionNodes.Item(0).InnerXml = description;
            }
            else
            {
                XmlNode newNode = xmlDocument.CreateElement("Description");
                newNode.InnerXml = description;
                nodesFiltered[0].AppendChild(newNode);
            }

        }


        private void btnSaveXML_Click(object sender, EventArgs e)
        {

            using (SaveFileDialog ofd = new SaveFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    xmlDocument.Save(ofd.FileName);
                    MessageBox.Show("Done");
                }
                else
                {
                }
            }
        }

        private void btnRunFunction_Click(object sender, EventArgs e)
        {
            functionDictionary[cmbFunction.Text](cmbLookIn.Text.ToString(), cmbFindFrom.Text.ToString(), txtFindValue.Text.ToString(), cmbReplaceFrom.Text.ToString(), txtReplaceValue.Text.ToString(), txtConditions.Text.ToString());
            MessageBox.Show("Done, remember to save");
            //DirectReplace(cmbLookIn.Text.ToString(), cmbFindFrom.Text.ToString(), txtFindValue.Text.ToString(), cmbReplaceFrom.Text.ToString(), txtReplaceValue.Text.ToString(), txtConditions.Text.ToString());
        }
    }


}

