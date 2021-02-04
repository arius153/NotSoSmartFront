using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartSaver
{
	public class CurrencyBehavior : Behavior<Entry>
	{
		private bool _hasFormattedOnce = false;
		protected override void OnAttachedTo(Entry entry)
		{
			entry.TextChanged += OnEntryTextChanged;
			entry.Focused += EntryOnFocused;
			entry.Unfocused += EntryOnUnfocused;
			base.OnAttachedTo(entry);
		}

		private void EntryOnUnfocused(object sender, FocusEventArgs e)
		{
			var entry = sender as Entry;
			if (entry?.Text.Length == 0 || string.IsNullOrEmpty(entry?.Text))
			{
				entry.Text = "0.00";
			}
		}

		private void EntryOnFocused(object sender, FocusEventArgs e)
		{
			var entry = sender as Entry;
			if (entry?.Text == "0.00")
			{
				entry.Text = "";
			}
		}

		protected override void OnDetachingFrom(Entry entry)
		{
			entry.TextChanged -= OnEntryTextChanged;
			entry.Focused -= EntryOnFocused;
			entry.Unfocused -= EntryOnUnfocused;
			base.OnDetachingFrom(entry);
		}

		private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
		{
			if (!_hasFormattedOnce && args.NewTextValue == "0")
			{
				((Entry)sender).Text = "0.00";
				_hasFormattedOnce = true;
			}
		}


	}
}
