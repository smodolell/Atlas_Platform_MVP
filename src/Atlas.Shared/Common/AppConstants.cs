namespace Atlas.Shared.Common;

public class AppConstants
{

    public const int CAT_EdoCivilId_Desconocido = 1;
    public const int CAT_EdoCivilId_Soltero = 2;
    public const int CAT_EdoCivilId_Casado = 3;
    public const int CAT_EdoCivilId_Viudo = 4;
    public const int CAT_EdoCivilId_Divorciado = 5;
    public const int CAT_EdoCivilId_Union_Libre = 6;
    public const int CAT_EdoCivilId_Comprometido = 7;

    public const int CAT_FormaPago_Descontado = 1;
    public const int CAT_FormaPago_PorPagar = 2;

    public const int CAT_GeneroId_FEMENINO = 1;
    public const int CAT_GeneroId_MASCULINO = 2;

    public const int CAT_ImpresionEstatus_PorCargar = 1;
    public const int CAT_ImpresionEstatus_EnRevision = 2;
    public const int CAT_ImpresionEstatus_Rechazado = 3;
    public const int CAT_ImpresionEstatus_Aprobado = 4;

    public const int CAT_TipoCalculo_MontoFijo = 1;
    public const int CAT_TipoCalculo_PorcentajeCapital = 2;
    public const int CAT_TipoCalculo_EquivaleNroPeriodos = 3;
    public const int CAT_TipoCalculo_PorcentajePeriodo = 4;


    public const int CAT_TipoDomicilioId_Casa = 1;
    public const int CAT_TipoDomicilioId_Trabajo = 2;
    public const int CAT_TipoDomicilioId_Otros = 3;

    public const int CAT_TipoPersonaId_PersonaFisica = 1;
    public const int CAT_TipoPersonaId_PersonaJuridia = 2;

    public const int FI_Perfil_Cliente = 1;

    public const int OT_PerfilId_Solicitante = 1;
    public const int OT_PerfilId_RepresentanteLegal = 2;
    public const int OT_PerfilId_Aval = 3;
    public const int OT_PerfilId_Garante = 4;

    public const int CAT_EstatusCreditoId_CAPTURADO = 1;
    public const int CAT_EstatusCreditoId_ACTIVO = 2;
    public const int CAT_EstatusCreditoId_TERMINADO = 3;

    public const int OT_TipoImpresion_Solicitud = 1;
    public const int OT_TipoImpresion_Kit = 2;

    public const int SYS_AccessPointType_Page = 1;
    public const int SYS_AccessPointType_Element = 2;
    public const int SYS_AccessPointType_LeftMenu = 0;


    public const int DocumentoEstatusId_Completo = 1;
    public const int DocumentoEstatusId_Incompleto = 2;
    public const int DocumentoEstatusId_Faltante = 3;
    public const int DocumentoEstatusId_EnRevision = 4;


    public const int SYS_Rol_WEBMASTER = 1;
    public const int SYS_Rol_ADMINISTRADOR = 2;
    public const int SYS_Rol_EJECUTIVO = 3;
    public const int SYS_Rol_SUBDIRECTOR = 4;
    public const int SYS_Rol_AUDITORIA = 5;
    public const int SYS_Rol_ANALISTA = 6;
    public const int SYS_Rol_INFORMÁTICA = 7;
    public const int SYS_Rol_FORMALIZADOR = 8;
    public const int SYS_Rol_GERENTE_DE_SUCURSAL = 9;



    public const int IV_PerfilPersona_PROVEEDOR = 1;
    public const int IV_PerfilPersona_CLIENTE = 2;

    public const int IV_TipoMovimiento_ENTRADA = 1;
    public const int IV_TipoMovimiento_SALIDA = 2;

    public const int PV_VentaEstatus_ABIERTA = 1;
    public const int PV_VentaEstatus_COMPLETADA = 2;
    public const int PV_VentaEstatus_CANCELADA = 3;

    public const int PV_FormaPago_EFECTIVO = 1;
    public const int PV_FormaPago_TARJETAS_DE_DEBITO = 2;
    public const int PV_FormaPago_TARJETAS_DE_CREDITO = 3;
    public const int PV_FormaPago_BILLETERAS_VIRTUALES = 4;
    public const int PV_FormaPago_TRANSFERENCIAS_BANCARIAS = 5;


    /// <summary>
    /// Borrador: La orden aún no ha sido enviada al proveedor y puede modificarse.
    /// </summary>
    public const int AC_OrdenCompraEstatus_Borrador = 1;

    /// <summary>
    /// Pendiente de Aprobación: La orden requiere revisión y aprobación antes de ser procesada.
    /// </summary>
    public const int AC_OrdenCompraEstatus_PendienteAprobacion = 2;

    /// <summary>
    /// Aprobada: La orden ha sido autorizada y está lista para ser enviada al proveedor.
    /// </summary>
    public const int AC_OrdenCompraEstatus_Aprobada = 3;

    /// <summary>
    /// Enviada: La orden ha sido enviada al proveedor, pero aún no se ha recibido confirmación.
    /// </summary>
    public const int AC_OrdenCompraEstatus_Enviada = 4;

    /// <summary>
    /// Aceptada: El proveedor ha confirmado la recepción de la orden y su disponibilidad para cumplirla.
    /// </summary>
    public const int AC_OrdenCompraEstatus_Aceptada = 5;

    /// <summary>
    /// En Proceso: El proveedor está preparando el pedido.
    /// </summary>
    public const int AC_OrdenCompraEstatus_EnProceso = 6;

    /// <summary>
    /// Parcialmente Recibida: Se ha recibido parte de la orden, pero aún falta completar la entrega.
    /// </summary>
    public const int AC_OrdenCompraEstatus_ParcialmenteRecibida = 7;

    /// <summary>
    /// Completada: Todos los productos o servicios han sido entregados y recibidos correctamente.
    /// </summary>
    public const int AC_OrdenCompraEstatus_Completada = 8;

    /// <summary>
    /// Cancelada: La orden ha sido anulada antes de su cumplimiento.
    /// </summary>
    public const int AC_OrdenCompraEstatus_Cancelada = 9;

    /// <summary>
    /// Rechazada: El proveedor no puede cumplir con la orden y la ha rechazado.
    /// </summary>
    public const int AC_OrdenCompraEstatus_Rechazada = 10;

    /// <summary>
    /// Facturada: La orden ha sido facturada y el pago está en proceso o ha sido completado.
    /// </summary>
    public const int AC_OrdenCompraEstatus_Facturada = 11;

    /// <summary>
    /// Cerrada: La orden ha sido completamente procesada y ya no requiere más acciones.
    /// </summary>
    public const int AC_OrdenCompraEstatus_Cerrada = 12;



    public const int PLA_CalculationDependency_Ratio = 1;




    public const string ContentTypeExcel = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    public const int COB_TipoRecordatorio_Lamada = 1;
    public const int COB_TipoRecordatorio_Cita = 2;


    public const int COB_Evento_PromesaPago = 1;
    public const int COB_Evento_AgendarLlamada = 2;
    public const int COB_Evento_AgendarCita = 3;

    public const int COB_TipoAsignacion_Criterios = 1;
    public const int COB_TipoAsignacion_Individual = 2;
    public const int COB_TipoAsignacion_Tranferencia = 3;
    

}

