using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Template.Core;

namespace Template.MVVM.Model
{
    public class Drug : ObservableObject
    {
        private string? _name;
        private string? _description;
        private string? _type;

        public List<DateTime?> DateTimes { get; set; } = new List<DateTime?>();


        public string? Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();

            }
        }

        public string? Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();

            }
        }

        public string? Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged();
            }

        }

        [JsonIgnore]
        public List<string> Types { get; } = new List<string>
        {
            "Injection",
            "Spray",
            "Tablet",
            "Capsule",
            "Syrup",
            "Mixture",
            "Inhaler",
            "Nebuliser"

        };

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name" && string.IsNullOrWhiteSpace(Name))
                {
                    return "Name is required.";
                }
                if (columnName == "Type" && string.IsNullOrWhiteSpace(Type))
                {
                    return "Type is required.";
                }
                return null;
            }

        }
            public string? Error => null;
    }
}



