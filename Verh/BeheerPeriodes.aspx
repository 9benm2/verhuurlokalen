<%@ Page Title="Beheer periodes" Language="C#" MasterPageFile="~/Verhuur.master" AutoEventWireup="true" CodeFile="BeheerPeriodes.aspx.cs" Inherits="BeheerPeriodes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(document).ready(function () {
            $(document).tooltip();
        })
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row clearfix">
        <div class="column full">
            <asp:Label SkinID="LabelMelding" ID="LabelMelding" runat="server" Text=""></asp:Label>

            <asp:GridView ID="GridViewPeriodes" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AllowPaging="True" OnPageIndexChanging="GridViewPeriodes_PageIndexChanging" PageSize="10" DataKeyNames="Id" OnSelectedIndexChanged="GridViewPeriodes_SelectedIndexChanged" CellSpacing="5">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="BeginPeriode" HeaderText="Begin periode" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="EindePeriode" HeaderText="Einde periode" DataFormatString="{0:d}" />

                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate><%#Eval("Type.Naam") %></ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate><%#Eval("Status.Naam") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField SelectText="Selecteer" ShowSelectButton="True" />
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#bafcbf" Font-Bold="True" ForeColor="Black" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
        </div>
    </div>
    <asp:Panel ID="Panel1" runat="server" Visible="false">
        <div class="row clearfix">
            <div class="column third">
                <h1>Informatie kamp</h1>
                <asp:Label SkinID="medLabel" ID="Label1" runat="server" Text="Begin periode"></asp:Label>
                <asp:TextBox ID="TextBoxBegin" runat="server" Width="180px"></asp:TextBox><br />
                <asp:Label SkinID="medLabel" ID="Label2" runat="server" Text="Einde periode"></asp:Label>
                <asp:TextBox ID="TextBoxEinde" runat="server" Width="180px"></asp:TextBox><br />
                <asp:Label SkinID="medLabel" ID="Label3" runat="server" Text="Type"></asp:Label>
                <asp:DropDownList ID="DropDownListType" runat="server" Width="180px">
                </asp:DropDownList><br />
                <asp:Label SkinID="medLabel" ID="Label4" runat="server" Text="Status"></asp:Label>
                <asp:DropDownList ID="DropDownListStatus" runat="server" Width="180px">
                </asp:DropDownList><br />
                <asp:Label SkinID="medLabel" ID="Label5" runat="server" Text="Vereniging"></asp:Label>
                <asp:TextBox ID="TextBoxVereniging" runat="server" Width="180px"></asp:TextBox><br />
                <asp:Label SkinID="medLabel" ID="Label9" runat="server" Text="Aantal personen"></asp:Label>
                <asp:TextBox ID="TextBoxPersonen" runat="server" Width="180px"></asp:TextBox><br />
                <asp:Label SkinID="medLabel" ID="Label10" runat="server" Text="Aantal tenten"></asp:Label>
                <asp:TextBox ID="TextBoxTenten" runat="server" Width="180px"></asp:TextBox><br />
                <asp:Label SkinID="medLabel" ID="Label11" runat="server" Text="Uur aankomst"></asp:Label>
                <asp:TextBox ID="TextBoxAankomst" runat="server" Width="180px"></asp:TextBox><br />
                <asp:Label SkinID="medLabel" ID="Label12" runat="server" Text="Uur vertrek"></asp:Label>
                <asp:TextBox ID="TextBoxVertrek" runat="server" Width="180px"></asp:TextBox><br />
                <asp:Label SkinID="medLabel" ID="Label13" runat="server" Text="Opmerkingen"></asp:Label>
                <asp:TextBox ID="TextBoxOpmerkingen" runat="server" TextMode="MultiLine" Width="335px" Height="150px"></asp:TextBox><br />

                <asp:Button ID="ButtonVerwijderen" runat="server" Text="Verwijderen" ForeColor="Red" OnClick="ButtonVerwijderen_Click" />
                <asp:Button ID="ButtonWijzigen" runat="server" Text="Opslaan" OnClick="ButtonWijzigen_Click" title="Veranderingen aan de kamporganisator worden niet doorgevoerd." />
            </div>
            <div class="column two-thirds">
                <h1>Betalingen</h1>
                <asp:Label SkinID="medLabel" ID="Label6" runat="server" Text="Waarborg"></asp:Label>
                <asp:TextBox ID="TextBoxWaarborg" runat="server" Width="100px"></asp:TextBox>
                <asp:DropDownList ID="DropDownListWaarborgStatus" runat="server" Width="180px">
                </asp:DropDownList><br />

                <asp:Label SkinID="medLabel" ID="Label7" runat="server" Text="Voorschot"></asp:Label>
                <asp:TextBox ID="TextBoxVoorschot" runat="server" Width="100px"></asp:TextBox>
                <asp:DropDownList ID="DropDownListVoorschotStatus" runat="server" Width="180px">
                </asp:DropDownList><br />

                <asp:Label SkinID="medLabel" ID="Label8" runat="server" Text="Restbedrag"></asp:Label>
                <asp:TextBox ID="TextBoxRestbedrag" runat="server" Width="100px"></asp:TextBox>
                <asp:DropDownList ID="DropDownListRestbedragStatus" runat="server" Width="180px">
                </asp:DropDownList><br />

                <!--placeholder met extra betalingen -->
                <asp:PlaceHolder runat="server" ID="phBetalingen" />
                <!---->
                <h1>Kamporganisator</h1>
                <asp:Label SkinID="medLabel" ID="Label21" runat="server" Text="Email"></asp:Label>
                <asp:TextBox ID="TextBoxEmail" runat="server" Width="285px"></asp:TextBox><br />
                <asp:Label SkinID="medLabel" ID="Label15" runat="server" Text="Voornaam"></asp:Label>
                <asp:TextBox ID="TextBoxVoornaam" runat="server" Width="285px"></asp:TextBox><br />
                <asp:Label SkinID="medLabel" ID="Label16" runat="server" Text="Naam"></asp:Label>
                <asp:TextBox ID="TextBoxNaam" runat="server" Width="285px"></asp:TextBox><br />
                <asp:Label SkinID="medLabel" ID="Label17" runat="server" Text="Adres"></asp:Label>
                <asp:TextBox ID="TextBoxAdres" runat="server" Width="285px"></asp:TextBox><br />
                <asp:Label SkinID="medLabel" ID="Label18" runat="server" Text="Postcode"></asp:Label>
                <asp:TextBox ID="TextBoxPostcode" runat="server" Width="285px"></asp:TextBox><br />
                <asp:Label SkinID="medLabel" ID="Label19" runat="server" Text="Plaats"></asp:Label>
                <asp:TextBox ID="TextBoxPlaats" runat="server" Width="285px"></asp:TextBox><br />
                <asp:Label SkinID="medLabel" ID="Label20" runat="server" Text="Telefoonnummer"></asp:Label>
                <asp:TextBox ID="TextBoxTelefoonnummer" runat="server" Width="285px"></asp:TextBox><br />

                <!--
                    <h1>ContractPersonen</h1>
                    placeholder met rest contractpersonen -->
                <asp:PlaceHolder runat="server" ID="phContractPersonen" />
            </div>
        </div>
    </asp:Panel>

</asp:Content>

