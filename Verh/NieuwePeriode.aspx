<%@ Page Title="Nieuwe periode" Language="C#" MasterPageFile="~/Verhuur.master" AutoEventWireup="true" CodeFile="NieuwePeriode.aspx.cs" Inherits="NieuwePeriode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(function () {
            $(".datepicker").datepicker();
            $(".datepicker").datepicker("option", "dateFormat", "dd/mm/yy");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row clearfix">
        <div class="column full">
            <div class="center320">
                <h2>Nieuwe periode</h2>
                <asp:Label ID="LabelBegin" runat="server" Text="Begin periode" Width="120px"></asp:Label><asp:TextBox ID="TextBoxBegin" CssClass="datepicker" runat="server" autofocus="true" Width="190px"></asp:TextBox><br />
                <asp:Label ID="LabelEinde" runat="server" Text="Einde periode" Width="120px"></asp:Label><asp:TextBox ID="TextBoxEinde" CssClass="datepicker" runat="server" Width="190px"></asp:TextBox><br />
                <asp:Label ID="LabelStatus" runat="server" Text="Status" Width="120px"></asp:Label><asp:DropDownList ID="DropDownListStatus" runat="server" Width="190px">
                </asp:DropDownList><br />
                <asp:Label ID="LabelType" runat="server" Text="Type" Width="120px"></asp:Label><asp:DropDownList ID="DropDownListType" runat="server" Width="190px">
                </asp:DropDownList><br />
                <asp:Label ID="Label1" runat="server" Text="" Width="120px"></asp:Label><asp:Button ID="ButtonPeriode" runat="server" Text="Periode maken" OnClick="ButtonPeriode_Click" />
            </div>
        </div>
    </div>
</asp:Content>
