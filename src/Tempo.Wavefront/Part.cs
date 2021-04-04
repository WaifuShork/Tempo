using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tempo.Wavefront
{
    public class Part : INotifyPropertyChanged
    {
        private bool _selected;
        private bool _visible = true;
        private bool _transparent;
        private DispMode _dispMode = DispMode.Normal;

        private static int _index;
        private string _name;

        public Part()
        {
            Index = _index++;
            _name = string.Format($"#part{Index}");
        }

        public List<Group> Groups = new List<Group>();
        public int Index { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public bool IsTexture { get; set; } = false;

        public bool Selected
        {
            get => _selected;
            set
            {
                foreach (var group in Groups)
                {
                    group.Selected = value;
                }

                _selected = value;
            }
        }

        public bool Visible
        {
            get => _visible;
            set
            {
                foreach (var group in Groups)
                {
                    group.Visible = value;
                }

                _visible = value;
            }
        }

        public bool Transparent
        {
            get => _transparent;
            set
            {
                foreach (var group in Groups)
                {
                    group.Transparent = value;
                }

                _transparent = value;
            }
        }

        public DispMode DisplayMode
        {
            get => _dispMode;
            set
            {
                switch (value)
                {
                    case DispMode.Normal:
                        Visible = true;
                        Transparent = false;
                        break;
                    case DispMode.Hide:
                        Visible = false;
                        break;
                    case DispMode.Transparent:
                        Transparent = true;
                        break;
                    default:
                        break;
                }

                _dispMode = value;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = default)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}