using BE;
using Services.Observer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Services
{
    public class Serializacion
    {
        public List<BECliente> Deseriaizar()
        {
            List<BECliente> listaClientes = new List<BECliente>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<BECliente>));
                    listaClientes = (List<BECliente>)serializer.Deserialize(fs);
                }
            }
            return listaClientes;
        }

        public string SerializarClientes(List<BECliente> listaClientes)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files|*.xml";
            saveFileDialog.FileName = DateTime.Now.ToString("yyyy-MM-dd HH_mm");
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<BECliente>));
                    serializer.Serialize(fs, listaClientes);
                }

                return saveFileDialog.FileName;
            }
            return null;
        }

    }
}
