﻿#pragma checksum "C:\Users\alonl\Desktop\AtariBallout\AtariBallout\AtariBallout\Views\StartScreenPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F5CF224EB5C0188BC54A0D6B023C7E8E05BDD9AEC4B5FC2480E4B7AAC270D8BB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AtariBallout.Views
{
    partial class StartScreenPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // Views\StartScreenPage.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                }
                break;
            case 2: // Views\StartScreenPage.xaml line 226
                {
                    this.LoginTitle_TextBlock_Style = (global::Windows.UI.Xaml.Style)(target);
                }
                break;
            case 13: // Views\StartScreenPage.xaml line 256
                {
                    this.ErrorMessage = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 14: // Views\StartScreenPage.xaml line 258
                {
                    this.User_TextBox_StartPage = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.User_TextBox_StartPage).KeyDown += this.User_TextBox_StartPage_KeyDown;
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.User_TextBox_StartPage).TextChanged += this.UsernameTextBox_TextChanged;
                }
                break;
            case 15: // Views\StartScreenPage.xaml line 260
                {
                    this.PasswordBox_StartPage = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                    ((global::Windows.UI.Xaml.Controls.PasswordBox)this.PasswordBox_StartPage).KeyDown += this.PasswordBox_StartPage_KeyDown;
                    ((global::Windows.UI.Xaml.Controls.PasswordBox)this.PasswordBox_StartPage).PasswordChanged += this.PasswordBox_StartPage_PasswordChanged;
                }
                break;
            case 16: // Views\StartScreenPage.xaml line 262
                {
                    this.PassportSignInButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.PassportSignInButton).Click += this.PassportSignInButton_Click;
                }
                break;
            case 17: // Views\StartScreenPage.xaml line 272
                {
                    this.RegisterButtonTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.RegisterButtonTextBlock).PointerPressed += this.RegisterButtonTextBlock_PointerPressed;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

