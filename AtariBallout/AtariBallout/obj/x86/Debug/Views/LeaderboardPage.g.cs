﻿#pragma checksum "C:\Users\alonl\Desktop\AtariBallout\AtariBallout\AtariBallout\Views\LeaderboardPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A207091A2CB84C0058BA32CA860BDF8BF98D224E43B06C4A02CB826740502296"
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
    partial class LeaderboardPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
            public static void Set_Windows_UI_Xaml_Documents_Run_Text(global::Windows.UI.Xaml.Documents.Run obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class LeaderboardPage_obj15_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            ILeaderboardPage_Bindings
        {
            private global::AtariBallout.Model.UserObject dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::System.WeakReference obj15;
            private global::Windows.UI.Xaml.Controls.TextBlock obj16;
            private global::Windows.UI.Xaml.Controls.TextBlock obj18;
            private global::Windows.UI.Xaml.Documents.Run obj19;
            private global::Windows.UI.Xaml.Documents.Run obj20;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj16TextDisabled = false;
            private static bool isobj18TextDisabled = false;
            private static bool isobj19TextDisabled = false;
            private static bool isobj20TextDisabled = false;

            public LeaderboardPage_obj15_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 145 && columnNumber == 37)
                {
                    isobj16TextDisabled = true;
                }
                else if (lineNumber == 148 && columnNumber == 95)
                {
                    isobj18TextDisabled = true;
                }
                else if (lineNumber == 147 && columnNumber == 126)
                {
                    isobj19TextDisabled = true;
                }
                else if (lineNumber == 147 && columnNumber == 178)
                {
                    isobj20TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 15: // Views\LeaderboardPage.xaml line 143
                        this.obj15 = new global::System.WeakReference((global::Windows.UI.Xaml.Controls.StackPanel)target);
                        break;
                    case 16: // Views\LeaderboardPage.xaml line 144
                        this.obj16 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 18: // Views\LeaderboardPage.xaml line 148
                        this.obj18 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 19: // Views\LeaderboardPage.xaml line 147
                        this.obj19 = (global::Windows.UI.Xaml.Documents.Run)target;
                        break;
                    case 20: // Views\LeaderboardPage.xaml line 147
                        this.obj20 = (global::Windows.UI.Xaml.Documents.Run)target;
                        break;
                    default:
                        break;
                }
            }

            public void DataContextChangedHandler(global::Windows.UI.Xaml.FrameworkElement sender, global::Windows.UI.Xaml.DataContextChangedEventArgs args)
            {
                 if (this.SetDataRoot(args.NewValue))
                 {
                    this.Update();
                 }
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Windows.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
                Recycle();
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
                switch(phase)
                {
                    case 0:
                        nextPhase = -1;
                        this.SetDataRoot(item);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            (this.obj15.Target as global::Windows.UI.Xaml.Controls.StackPanel).DataContextChanged -= this.DataContextChangedHandler;
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_((global::AtariBallout.Model.UserObject) item, 1 << phase);
            }

            public void Recycle()
            {
            }

            // ILeaderboardPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::AtariBallout.Model.UserObject)newDataRoot;
                    return true;
                }
                return false;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::AtariBallout.Model.UserObject obj, int phase)
            {
                LeaderboardPage_obj15_Bindings bindings = this;
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_Username(obj.Username, phase);
                        this.Update_Highscore(obj.Highscore, phase);
                        this.Update_FirstName(obj.FirstName, phase);
                        this.Update_LastName(obj.LastName, phase);
                    }
                }
            }
            private void Update_Username(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\LeaderboardPage.xaml line 144
                    if (!isobj16TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj16, obj, null);
                    }
                }
            }
            private void Update_Highscore(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\LeaderboardPage.xaml line 148
                    if (!isobj18TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj18, obj.ToString(), null);
                    }
                }
            }
            private void Update_FirstName(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\LeaderboardPage.xaml line 147
                    if (!isobj19TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Documents_Run_Text(this.obj19, obj, null);
                    }
                }
            }
            private void Update_LastName(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\LeaderboardPage.xaml line 147
                    if (!isobj20TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Documents_Run_Text(this.obj20, obj, null);
                    }
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // Views\LeaderboardPage.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                }
                break;
            case 11: // Views\LeaderboardPage.xaml line 92
                {
                    this.ContentArea = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 12: // Views\LeaderboardPage.xaml line 109
                {
                    this.InventoryList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 21: // Views\LeaderboardPage.xaml line 102
                {
                    this.ReadAloud_Button = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.ReadAloud_Button).Click += this.ReadAloud_Button_Click;
                }
                break;
            case 22: // Views\LeaderboardPage.xaml line 104
                {
                    this.ReadAloud_Button_SymbolIcon = (global::Windows.UI.Xaml.Controls.SymbolIcon)(target);
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
            switch(connectionId)
            {
            case 15: // Views\LeaderboardPage.xaml line 143
                {                    
                    global::Windows.UI.Xaml.Controls.StackPanel element15 = (global::Windows.UI.Xaml.Controls.StackPanel)target;
                    LeaderboardPage_obj15_Bindings bindings = new LeaderboardPage_obj15_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(element15.DataContext);
                    element15.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element15, bindings);
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element15, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}
