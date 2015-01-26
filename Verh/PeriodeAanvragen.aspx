<%@ Page Title="" Language="C#" MasterPageFile="~/Verhuur.master" AutoEventWireup="true" CodeFile="PeriodeAanvragen.aspx.cs" Inherits="PeriodeAanvragen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(document).ready(function () {
            $(document).tooltip();
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p><asp:Label SkinID="LabelMelding" ID="LabelMelding" runat="server" Text=""></asp:Label></p>
    <p>
        Als u een periode aanvraagt, heeft u gedurende 7 dagen een optie op de periode. Als u de optie gedurende die 7 dagen niet bevestigde, vervalt deze.
    </p>
    <p>
        <asp:Label SkinID="medLabel" ID="Label1" runat="server" Text="Begin periode"></asp:Label><asp:TextBox ID="TextBoxBegin" runat="server"></asp:TextBox>
        <br />
        <asp:Label SkinID="medLabel" ID="Label2" runat="server" Text="Begin periode"></asp:Label><asp:TextBox ID="TextBoxEinde" runat="server"></asp:TextBox>
        <br />
        <asp:Label SkinID="medLabel" ID="LabelDefinitief" runat="server" Text="Definitief (?)" title="Als u dit aanvinkt wordt de periode als definitief gemaakt (betaling is verplicht). Als u dit nog niet aanvinkt, kan u gedurdende 7 dagen de periode nog op definitief zetten. Gedurende deze 7 dagen is de periode voor u gereserveerd. Na 7 dagen vervalt de reservatie"></asp:Label>
        <asp:CheckBox ID="CheckBoxDefinitief" runat="server" />
        <br />
        <asp:Label SkinID="medLabel" ID="Label4" runat="server" Text="Naam vereniging"></asp:Label><asp:TextBox ID="TextBoxVereniging" runat="server"></asp:TextBox>
        <br />
        <asp:Label SkinID="medLabel" ID="Label9" runat="server" Text="Aantal personen"></asp:Label><asp:TextBox ID="TextBoxPersonen" runat="server" OnTextChanged="TextBoxPersonen_TextChanged" AutoPostBack="True"></asp:TextBox>
        <br />
        <asp:Label SkinID="medLabel" ID="Label7" runat="server" Text="Aantal tenten"></asp:Label><asp:TextBox ID="TextBoxTenten" runat="server"></asp:TextBox>
        <br />
        <asp:Label SkinID="medLabel" ID="Label5" runat="server" Text="Tijdstip aankomst"></asp:Label><asp:TextBox ID="TextBoxAankomst" runat="server"></asp:TextBox>
        <br />
        <asp:Label SkinID="medLabel" ID="Label6" runat="server" Text="Tijdstip vertrek"></asp:Label><asp:TextBox ID="TextBoxVertrek" runat="server"></asp:TextBox>
        <br />
        <asp:Label SkinID="medLabel" ID="Label8" runat="server" Text="Opmerkingen" Font-Strikeout="False"></asp:Label><asp:TextBox ID="TextBoxOpmerkingen" runat="server" TextMode="MultiLine" Width="400px" Height="100px"></asp:TextBox><br />
        <asp:Label SkinID="medLabel" ID="Label10" runat="server" Text="Kostprijs (?)" title="De prijs is minimum 2500 euro, omdat we ons op grote groepen en langere periodes willen focussen. De prijs per persoon per nacht is 3,5 euro."></asp:Label><asp:TextBox ID="TextBoxKostprijs" runat="server" Enabled="False"></asp:TextBox>
        <asp:Label ID="Label12" runat="server" Text="+ kosten verbruik"></asp:Label><asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/ContractPagina.aspx">(zie contract)</asp:HyperLink>
        <br />
        <asp:Label SkinID="medLabel" ID="Label11" runat="server" Text="Waarborg"></asp:Label><asp:TextBox ID="TextBoxWaarborg" runat="server" Enabled="False"></asp:TextBox>
        <br />

        <br />

        <br />
        <asp:Label SkinID="medLabel" ID="Label3" runat="server" Text=""></asp:Label><asp:Button ID="Button1" runat="server" Text="Periode aanvragen" OnClick="Button1_Click" />
    </p>
</asp:Content>