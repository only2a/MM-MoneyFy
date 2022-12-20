﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MM_MoneyFy.Validation
{
	public static class RegexValidator
	{
		#region Static Constructor

		static RegexValidator()
		{
			RegexTextProperty = DependencyProperty.RegisterAttached(
				"RegexText",
				typeof(string),
				typeof(RegexValidator),
				new UIPropertyMetadata(null, OnAttachedPropertyChanged));

			ErrorMessageProperty = DependencyProperty.RegisterAttached(
				"ErrorMessage",
				typeof(string),
				typeof(RegexValidator),
				new UIPropertyMetadata(null, OnAttachedPropertyChanged));
		}

		#endregion // Static Constructor

		#region Attached Properties

		#region ErrorMessage

		/// <summary>
		/// Identifies the RegexValidator's ErrorMessage attached property.  
		/// This field is read-only.
		/// </summary>
		public static readonly DependencyProperty ErrorMessageProperty;

		/// <summary>
		/// Returns the error message used when validation fails for the
		/// specified TextBox.
		/// </summary>
		/// <param name="textBox">The TextBox whose error message is returned.</param>
		public static string GetErrorMessage(TextBox textBox)
		{
			return textBox.GetValue(ErrorMessageProperty) as string;
		}

		/// <summary>
		/// Sets the error message used when validation fails for the 
		/// specified TextBox.
		/// </summary>
		/// <param name="textBox">The TextBox being validated.</param>
		/// <param name="value">The error message.</param>
		public static void SetErrorMessage(TextBox textBox, string value)
		{
			textBox.SetValue(ErrorMessageProperty, value);
		}

		#endregion // ErrorMessage

		#region RegexText

		/// <summary>
		/// Identifies the RegexValidator's RegexText attached property.  
		/// This field is read-only.
		/// </summary>
		public static readonly DependencyProperty RegexTextProperty;

		/// <summary>
		/// Returns the regular expression used to validate the specified TextBox.
		/// </summary>
		/// <param name="textBox">The TextBox whose regular expression is returned.</param>
		public static string GetRegexText(TextBox textBox)
		{
			return textBox.GetValue(RegexTextProperty) as string;
		}

		/// <summary>
		/// Sets the regular expression used to validate the 
		/// specified TextBox.
		/// </summary>
		/// <param name="textBox">The TextBox being validated.</param>
		/// <param name="value">The regular expression.</param>
		public static void SetRegexText(TextBox textBox, string value)
		{
			textBox.SetValue(RegexTextProperty, value);
		}

		#endregion // RegexText

		#endregion // Attached Properties

		#region Event Handling Methods

		#region OnAttachedPropertyChanged

		/// <summary>
		/// Invoked whenever an attached property of the 
		/// RegexValidator is modified for a TextBox.
		/// </summary>
		/// <param name="depObj">A TextBox.</param>
		/// <param name="e"></param>
		static void OnAttachedPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
		{
			TextBox textBox = depObj as TextBox;
			if (textBox == null)
				throw new InvalidOperationException(
					"The RegexValidator can only be used with a TextBox.");

			VerifyRegexValidationRule(textBox);
		}

		#endregion // OnAttachedPropertyChanged

		#endregion // Event Handling Methods

		#region Private Helpers

		#region GetRegexValidationRuleForTextBox

		/// <summary>
		/// Returns a RegexValidationRule to be used for validating the specified TextBox.
		/// If the TextBox is not yet initialized, this method returns null.
		/// </summary>
		static RegexValidationRule GetRegexValidationRuleForTextBox(TextBox textBox)
		{
			if (!textBox.IsInitialized)
			{
				// If the TextBox.Text property is bound, but the TextBox is not yet
				// initialized, the property's binding can be null.  In that situation,
				// hook its Initialized event and verify the validation rule again when 
				// that event fires.  At that point in time, the Text property's binding
				// will be non-null.
				EventHandler callback = null;
				callback = delegate
				{
					textBox.Initialized -= callback;
					VerifyRegexValidationRule(textBox);
				};
				textBox.Initialized += callback;
				return null;
			}

			// Get the binding expression associated with the TextBox's Text property.
			BindingExpression expression = textBox.GetBindingExpression(TextBox.TextProperty);
			if (expression == null)
				throw new InvalidOperationException(
					"The TextBox's Text property must be bound for the RegexValidator to validate it.");

			// Get the binding which owns the binding expression.
			Binding binding = expression.ParentBinding;
			if (binding == null)
				throw new ApplicationException(
					"Unexpected situation: the TextBox.Text binding expression has no parent binding.");

			// Look for an existing instance of the RegexValidationRule class in the
			// binding.  If there is more than one instance in the ValidationRules
			// then throw an exception because we don't know which one to modify.
			RegexValidationRule regexRule = null;
			foreach (ValidationRule rule in binding.ValidationRules)
			{
				if (rule is RegexValidationRule)
				{
					if (regexRule == null)
						regexRule = rule as RegexValidationRule;
					else
						throw new InvalidOperationException(
							"There should not be more than one RegexValidationRule in a Binding's ValidationRules.");
				}
			}

			// If the TextBox.Text property's binding does not yet have an 
			// instance of RegexValidationRule in its ValidationRules,
			// add an instance now and return it.
			if (regexRule == null)
			{
				regexRule = new RegexValidationRule();
				binding.ValidationRules.Add(regexRule);
			}

			return regexRule;
		}

		#endregion // GetRegexValidationRuleForTextBox

		#region VerifyRegexValidationRule

		/// <summary>
		/// Creates or modifies the RegexValidationRule in the TextBox's 
		/// Text property binding to use the current values of the attached 
		/// properties exposed by this class.
		/// </summary>
		/// <param name="textBox">The TextBox being validated.</param>
		static void VerifyRegexValidationRule(TextBox textBox)
		{
			RegexValidationRule regexRule = GetRegexValidationRuleForTextBox(textBox);
			if (regexRule != null)
			{
				regexRule.RegexText =
					textBox.GetValue(RegexValidator.RegexTextProperty) as string;

				regexRule.ErrorMessage =
					textBox.GetValue(RegexValidator.ErrorMessageProperty) as string;
			}
		}

		#endregion // VerifyRegexValidationRule

		#endregion // Private Helpers
	}
}
