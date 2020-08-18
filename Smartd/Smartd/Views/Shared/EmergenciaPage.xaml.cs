﻿using Smartd.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smartd.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmergenciaPage
    {
        public EmergenciaPage()
        {
            InitializeComponent();
            BindingContext = new EmergenciaVM(this);
        }
    }
}