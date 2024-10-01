<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Departamento_Modificar.aspx.cs" Inherits="FrameWork.Departamento.Departamento_Modificar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    
    <asp:Label ID="lblTitulo" runat="server" CssClass="fs-4 fw-bold"></asp:Label>

    <div class="mb-3">
        <label class="form-label">Id</label>
        <asp:TextBox ID="txtDepartamentoId" runat="server" CssClass="form-control" ReadOnly BackColor="#FF3300"></asp:TextBox>
    </div>

      <div class="mb-3">
      <label class="form-label">Nombre</label>
      <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ></asp:TextBox>
  </div>

        <div class="mb-3">
        <label class="form-label">Estado</label>
        <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control" ReadOnly BackColor="#FF3300"></asp:TextBox>
    </div>

    <asp:Button ID="btnSubmit" runat="server" Text="Enviar" CssClass="btn btn-sm btn-primary" OnClick="btnSubmit_Click" />
    <asp:LinkButton runat="server" PostBackUrl="~/Departamento/Departamento_Listado.aspx"  CssClass="btn btn-sm btn-warning">Volver</asp:LinkButton>
    



</asp:Content>
