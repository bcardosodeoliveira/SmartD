using Xamarin.Forms;

namespace Smartd.Bibliotecas.Effects
{
    public class ShowHidePassEffect : RoutingEffect
	{
		public Color Color { get; set; }
		public ShowHidePassEffect() : base("Xamarin.ShowHidePassEffect") { }
	}
}
