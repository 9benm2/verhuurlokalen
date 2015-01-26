<%@ Page Title="Verhuur lokalen Chiro Veerle" Language="C#" MasterPageFile="~/Verhuur.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row clearfix">
        <div class="column half">
            <asp:Label SkinID="LabelMelding" ID="LabelMelding" runat="server" Text=""></asp:Label>
            <p>
                Een splinternieuw jeugdlokaal, gelegen in een rustige omgeving met voldoende sportinfrastructuur.
                Zelfs een tentenkamp opstellen is er mogelijk.
            </p>
            <p>
                Voor 95 personen is er in het gebouw slaapgelegenheid op een totaal capaciteit van 
135 personen.
            </p>
            <p>
                Via de kalender kan u een optie nemen op een beschikbare periode.<br />
                Wil u later afzien van uw genomen optie kan u deze annuleren.
            </p>
        </div>
        <div class="column half">
            <div class="calcenter panel">
                <asp:Calendar ID="Calendar" runat="server" OnSelectionChanged="Calendar_SelectionChanged" OnDayRender="Calendar_DayRender"></asp:Calendar>
                <asp:Panel ID="PanelPeriodeNemen" runat="server">
                    <div class="row clearfix">
                        <div class="column full">
                            <asp:Button ID="ButtonBegin" runat="server" Text="Als begin vastleggen" OnClick="ButtonBegin_Click" />
                            <asp:Label ID="LabelBegin" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="column full">
                            <asp:Button ID="ButtonEinde" runat="server" Text="Als einde vastleggen" OnClick="ButtonEinde_Click" />
                            <asp:Label ID="LabelEinde" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="column full">
                            <asp:Button ID="ButtonBevestigen" runat="server" Text="Bevestigen" OnClick="ButtonBevestigen_Click" /><asp:Label ID="LabelFout" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
                <p>
                    <asp:Label ID="LabelCalendar" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Solid"></asp:Label>
                </p>

            </div>
        </div>
    </div>
</asp:Content>

