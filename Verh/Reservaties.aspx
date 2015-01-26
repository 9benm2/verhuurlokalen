<%@ Page Title="" Language="C#" MasterPageFile="~/Verhuur.master" AutoEventWireup="true" CodeFile="Reservaties.aspx.cs" Inherits="Reservaties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p>
        <asp:Label SkinID="LabelMelding" ID="LabelMelding" runat="server" Text=""></asp:Label>
    </p>
    <p>
        <div class="row clearfix">
            <div class="column half">
                <asp:Panel ID="Panel1" runat="server"></asp:Panel>
            </div>
            <div class="column half">
                <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
            </div>
        </div>
    </p>
</asp:Content>

