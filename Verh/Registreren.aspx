<%@ Page Title="Registreren" Language="C#" MasterPageFile="~/Verhuur.master" AutoEventWireup="true" CodeFile="Registreren.aspx.cs" Inherits="Registreer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row clearfix">
        <div class="column full">
            <div class="center320">
                <asp:Label SkinID="LabelMelding" ID="LabelMelding" runat="server" Text=""></asp:Label>
                <asp:Label ID="LabelEmail" runat="server" Text="E-mail" Width="110px"></asp:Label><asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox><br />
                <asp:Label ID="LabelWachtwoord" runat="server" Text="Wachtwoord" Width="110px"></asp:Label><asp:TextBox ID="TextBoxWachtwoord" runat="server" TextMode="Password"></asp:TextBox><br />
                <asp:Label ID="LabelVoornaam" runat="server" Text="Voornaam" Width="110px"></asp:Label><asp:TextBox ID="TextBoxVoornaam" runat="server"></asp:TextBox><br />
                <asp:Label ID="LabelNaam" runat="server" Text="Naam" Width="110px"></asp:Label><asp:TextBox ID="TextBoxNaam" runat="server"></asp:TextBox><br />
                <asp:Label ID="LabelAdres" runat="server" Text="Adres" Width="110px"></asp:Label><asp:TextBox ID="TextBoxAdres" runat="server"></asp:TextBox><br />
                <asp:Label ID="LabelPostcode" runat="server" Text="Postcode" Width="110px"></asp:Label><asp:TextBox ID="TextBoxPostcode" runat="server" TextMode="SingleLine"></asp:TextBox><br />
                <asp:Label ID="LabelPlaats" runat="server" Text="Plaats" Width="110px"></asp:Label><asp:TextBox ID="TextBoxPlaats" runat="server"></asp:TextBox><br />
                <asp:Label ID="LabelTelefoonnummer" runat="server" Text="Tel./Gsm" Width="110px"></asp:Label><asp:TextBox ID="TextBoxTelefoonnummer" runat="server"></asp:TextBox><br />
                <asp:Label ID="Label1" runat="server" Text="" Width="110px"></asp:Label><asp:Button ID="ButtonRegistreer" runat="server" Text="Registreer" OnClick="ButtonRegistreer_Click" />
            </div>
        </div>
    </div>
</asp:Content>

