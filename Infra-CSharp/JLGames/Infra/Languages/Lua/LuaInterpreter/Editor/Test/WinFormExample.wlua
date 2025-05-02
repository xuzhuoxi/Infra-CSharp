form = Gui.Form{
    Text="Main Form", Height=200, StartPosition="CenterScreen",
    Gui.Label{ Text="Hello!", Name="lable", Width=80, Height=17, Top=9, Left=12 },
    Gui.Button{ Text="Click", Width=80, Height=23, Top=30, Left=12,
        Click=function(sender,e) Gui.ShowMessage(lable.Text,"Clicked") end },
}
Gui.Run(form)