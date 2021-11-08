<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard/DashBoardCore.Master" AutoEventWireup="true" CodeBehind="Blank.aspx.cs" Inherits="MemoEngine.Blank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb my-0 ms-2">
                <li class="breadcrumb-item">
                    <!-- if breadcrumb is single-->
                    <span>Home</span>
                </li>
                <li class="breadcrumb-item active"><span>Blank</span></li>
            </ol>
        </nav>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
