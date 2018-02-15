using System;
using System.Xml.Linq;
using System.Web.Hosting;
using System.Linq;
using System.Xml;
using Models = AppointmentEntry.Pocos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppointmentEntry.Services
{
    public static class Appointment
    {
        public static string DataPath = HostingEnvironment.MapPath( "~/XML/Appt.xml");

        public static string GetAll() {

            XmlDocument data = new XmlDocument();
            data.Load(DataPath);

            return JsonConvert.SerializeXmlNode(data);
        }

        public static string GetBy(string title) {
            try
            {
                XmlDocument data = new XmlDocument();
                data.Load(DataPath);

                XmlElement node = (XmlElement)data.SelectSingleNode("//Appointments/Appointment[Title='" + title + "']");

                var output = node.ChildNodes[0].InnerText + "|" + 
                        node.ChildNodes[1].InnerText + "|" + 
                        node.ChildNodes[2].InnerText + "|" +
                        node.ChildNodes[3].InnerText + "|" +
                        node.ChildNodes[4].InnerText + "|" +
                        node.ChildNodes[5].InnerText + "|";

                return output;
            }
            catch (Exception e) {
                return "";
            }
        }

        public static bool Insert(Models.Appointment Appointment)
        {
            try
            {
                XmlDocument data = new XmlDocument();
                data.Load(DataPath);

                //create node and add value
                XmlNode node = data.CreateNode(XmlNodeType.Element, "Appointment", null);

                //create title node
                XmlNode nodeTitle = data.CreateElement("Title");
                nodeTitle.InnerText = Appointment.Title;

                //create Description node
                XmlNode nodeDescription = data.CreateElement("Description");
                nodeDescription.InnerText = Appointment.Description;

                //create Date node
                XmlNode nodeDate = data.CreateElement("Date");
                nodeDate.InnerText = Appointment.Date.ToString();

                //create Time node
                XmlNode nodeTime = data.CreateElement("Time");
                nodeTime.InnerText = Appointment.Time.Replace("|", ":");

                //create Type node
                XmlNode nodeType = data.CreateElement("Type");
                nodeType.InnerText = Appointment.Type;

                //create Description node
                XmlNode nodeWorkRelated = data.CreateElement("WorkRelated");
                nodeWorkRelated.InnerText = Appointment.WorkRelated;
                
                // Add all the appointment details to appointment node
                node.AppendChild(nodeTitle);
                node.AppendChild(nodeDescription);
                node.AppendChild(nodeDate);
                node.AppendChild(nodeTime);
                node.AppendChild(nodeType);
                node.AppendChild(nodeWorkRelated);

                // Insert the new appointment to the XML file
                data.DocumentElement.AppendChild(node);

                //save back
                data.Save(DataPath);

                return true;

            }catch(Exception e){
                return false;
            }
        }

        public static bool Update(Models.Appointment Appointment)
        {
            try
            {
                XmlDocument data = new XmlDocument();
                data.Load(DataPath);

                XmlElement node = (XmlElement)data.SelectSingleNode("//Appointments/Appointment[Title='" + Appointment.Title + "']");
                if (node != null)
                {

                    node.ChildNodes[1].InnerText = Appointment.Description;
                    node.ChildNodes[2].InnerText = Appointment.Date.ToString();
                    node.ChildNodes[3].InnerText = Appointment.Time;
                    node.ChildNodes[4].InnerText = Appointment.Type;
                    node.ChildNodes[5].InnerText = Appointment.WorkRelated;
                }
                data.Save(DataPath);

                return true;
            }
            catch (Exception e) {
                return false;
            }
            
        }

        public static bool Delete(string title)
        {
			try
			{
                XmlDocument data = new XmlDocument();
                data.Load(DataPath);

                XmlElement node = (XmlElement)data.SelectSingleNode("//Appointments/Appointment[Title='"+ title + "']");
				if (node != null) { node.ParentNode.RemoveChild(node); }
				data.Save(DataPath);

				return true;

			}
			catch (Exception e)
			{
				return false;
			}

		}

    }
}
