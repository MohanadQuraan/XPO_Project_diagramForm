<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="Default.aspx.cs" Inherits="MainProject._Default" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

        <div>
            <div style="margin:10px">
                <a href="Default.aspx" style="font-size: 30px; text-decoration: none;"><strong>APEX</strong>  Local Hospital System</a>
            </div>

            <div style="margin:20px;">
<dx:ASPxMenu Paddings-Padding="20" ID="ASPxMenu1" runat="server" ShowAsToolbar="true" ShowPopOutImages="true">
    <Items>

        <dx:MenuItem ItemStyle-Paddings-Padding="20" Text="Problems Page" NavigateUrl="ProblemForm.aspx">                       

        </dx:MenuItem>

        <dx:MenuItem ItemStyle-Paddings-Padding="20" Text="Questions Page" NavigateUrl="QuestionsForm.aspx">                     


        </dx:MenuItem>
    </Items>
</dx:ASPxMenu>

            </div>
            

        </div>
    
</asp:Content>