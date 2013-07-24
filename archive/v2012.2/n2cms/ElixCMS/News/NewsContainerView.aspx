<%@ Page Language="C#" MasterPageFile="~/Templates/UI/Layouts/Top+SubMenu.master" AutoEventWireup="true" CodeBehind="NewsContainerView.aspx.cs" Inherits="ElixCMS.News.NewsContainerPage" Title="" %>
<asp:Content ContentPlaceHolderID="PostContent" runat="server">
<asp:PlaceHolder runat="server"  ID="container"></asp:PlaceHolder>
    <%--<asp:Repeater runat="server" ID="rNews">
        <HeaderTemplate><div class="list"></HeaderTemplate>
        <ItemTemplate>
            <div class="item i<%# Container.ItemIndex %> a<%# Container.ItemIndex % 2 %>">
                <span class="date"><%# Eval("Published") %></span>
                <a href='<%# Eval("Url") %>'><%# Eval("Title") %></a>
                <p><%# Eval("Introduction") %></p>
            </div>
        </ItemTemplate>
        <FooterTemplate></div></FooterTemplate>
    </asp:Repeater>--%>
</asp:Content>
