using System;
using System.Text;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace PathConverter
{
    public partial class Form1 : Form
    {
        public Form1() => InitializeComponent();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                outputBox.Text = Convert(rawDataBox.Text);
            }
            catch (Exception ex)
            {
                outputBox.Text = ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e) => rawDataBox.Text = string.Empty;

        private void button3_Click(object sender, EventArgs e) => Clipboard.SetText(outputBox.Text);

        private static string Convert(string rawData)
        {
            rawData = rawData.Replace("GenericPropertyJSON:", string.Empty);
            GenericProperty property = JsonConvert.DeserializeObject<GenericProperty>(rawData);
            int size = (int)property.children[0].children[0].val;
            StringBuilder sb = new StringBuilder();
            sb.Append($"new Vector2[]{Environment.NewLine}{{");
            for (int i = 1; i < size + 1; i++)
            {
                sb.Append($"{Environment.NewLine}   new Vector2({property.children[0].children[i].children[0].val.ToString().Replace(',', '.')}f, {property.children[0].children[i].children[1].val.ToString().Replace(',', '.')}f)");
                if (i != size) sb.Append(",");
            }
            sb.Append($"{Environment.NewLine}}};");
            return sb.ToString();
        }

        private class GenericProperty
        {
            public string name;
            public int type;
            public int arraySize;
            public string arrayType;
            public Children[] children;
        }

        private class Children
        {
            public string name;
            public int type;
            public int arraySize;
            public string arrayType;
            public SubChildren[] children;
            public float val;
        }

        private class SubChildren
        {
            public string name;
            public int type;
            public float val;
            public SubChildren[] children;
        }
    }
}
