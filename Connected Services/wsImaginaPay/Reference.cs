﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaLibreriaImagina.wsImaginaPay {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TransaccionDTO", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class TransaccionDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private long ID_TRANSACCIONField;
        
        private decimal TOTAL_TRANSACCIONField;
        
        private long PEDIDO_IDField;
        
        private bool APROBADOField;
        
        private System.DateTime FECHAField;
        
        private long METODO_PAGO_IDField;
        
        private long USUARIO_IDField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public long ID_TRANSACCION {
            get {
                return this.ID_TRANSACCIONField;
            }
            set {
                if ((this.ID_TRANSACCIONField.Equals(value) != true)) {
                    this.ID_TRANSACCIONField = value;
                    this.RaisePropertyChanged("ID_TRANSACCION");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public decimal TOTAL_TRANSACCION {
            get {
                return this.TOTAL_TRANSACCIONField;
            }
            set {
                if ((this.TOTAL_TRANSACCIONField.Equals(value) != true)) {
                    this.TOTAL_TRANSACCIONField = value;
                    this.RaisePropertyChanged("TOTAL_TRANSACCION");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public long PEDIDO_ID {
            get {
                return this.PEDIDO_IDField;
            }
            set {
                if ((this.PEDIDO_IDField.Equals(value) != true)) {
                    this.PEDIDO_IDField = value;
                    this.RaisePropertyChanged("PEDIDO_ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public bool APROBADO {
            get {
                return this.APROBADOField;
            }
            set {
                if ((this.APROBADOField.Equals(value) != true)) {
                    this.APROBADOField = value;
                    this.RaisePropertyChanged("APROBADO");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=4)]
        public System.DateTime FECHA {
            get {
                return this.FECHAField;
            }
            set {
                if ((this.FECHAField.Equals(value) != true)) {
                    this.FECHAField = value;
                    this.RaisePropertyChanged("FECHA");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=5)]
        public long METODO_PAGO_ID {
            get {
                return this.METODO_PAGO_IDField;
            }
            set {
                if ((this.METODO_PAGO_IDField.Equals(value) != true)) {
                    this.METODO_PAGO_IDField = value;
                    this.RaisePropertyChanged("METODO_PAGO_ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=6)]
        public long USUARIO_ID {
            get {
                return this.USUARIO_IDField;
            }
            set {
                if ((this.USUARIO_IDField.Equals(value) != true)) {
                    this.USUARIO_IDField = value;
                    this.RaisePropertyChanged("USUARIO_ID");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="wsImaginaPay.ImaginaPaySoap")]
    public interface ImaginaPaySoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CreatePayment", ReplyAction="*")]
        int CreatePayment(decimal total, long pedido_id, long metodo_pago_id, long usuario_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CreatePayment", ReplyAction="*")]
        System.Threading.Tasks.Task<int> CreatePaymentAsync(decimal total, long pedido_id, long metodo_pago_id, long usuario_id);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento GetPaymentDetailsResult del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetPaymentDetails", ReplyAction="*")]
        SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsResponse GetPaymentDetails(SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetPaymentDetails", ReplyAction="*")]
        System.Threading.Tasks.Task<SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsResponse> GetPaymentDetailsAsync(SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento user_rut del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetPaymentHistory", ReplyAction="*")]
        SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryResponse GetPaymentHistory(SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetPaymentHistory", ReplyAction="*")]
        System.Threading.Tasks.Task<SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryResponse> GetPaymentHistoryAsync(SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento user_rut del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CreateBranchPayment", ReplyAction="*")]
        SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentResponse CreateBranchPayment(SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CreateBranchPayment", ReplyAction="*")]
        System.Threading.Tasks.Task<SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentResponse> CreateBranchPaymentAsync(SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPaymentDetailsRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPaymentDetails", Namespace="http://tempuri.org/", Order=0)]
        public SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsRequestBody Body;
        
        public GetPaymentDetailsRequest() {
        }
        
        public GetPaymentDetailsRequest(SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetPaymentDetailsRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long id;
        
        public GetPaymentDetailsRequestBody() {
        }
        
        public GetPaymentDetailsRequestBody(long id) {
            this.id = id;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPaymentDetailsResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPaymentDetailsResponse", Namespace="http://tempuri.org/", Order=0)]
        public SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsResponseBody Body;
        
        public GetPaymentDetailsResponse() {
        }
        
        public GetPaymentDetailsResponse(SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetPaymentDetailsResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public SistemaLibreriaImagina.wsImaginaPay.TransaccionDTO GetPaymentDetailsResult;
        
        public GetPaymentDetailsResponseBody() {
        }
        
        public GetPaymentDetailsResponseBody(SistemaLibreriaImagina.wsImaginaPay.TransaccionDTO GetPaymentDetailsResult) {
            this.GetPaymentDetailsResult = GetPaymentDetailsResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPaymentHistoryRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPaymentHistory", Namespace="http://tempuri.org/", Order=0)]
        public SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryRequestBody Body;
        
        public GetPaymentHistoryRequest() {
        }
        
        public GetPaymentHistoryRequest(SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetPaymentHistoryRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string user_rut;
        
        public GetPaymentHistoryRequestBody() {
        }
        
        public GetPaymentHistoryRequestBody(string user_rut) {
            this.user_rut = user_rut;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPaymentHistoryResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPaymentHistoryResponse", Namespace="http://tempuri.org/", Order=0)]
        public SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryResponseBody Body;
        
        public GetPaymentHistoryResponse() {
        }
        
        public GetPaymentHistoryResponse(SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetPaymentHistoryResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public SistemaLibreriaImagina.wsImaginaPay.TransaccionDTO[] GetPaymentHistoryResult;
        
        public GetPaymentHistoryResponseBody() {
        }
        
        public GetPaymentHistoryResponseBody(SistemaLibreriaImagina.wsImaginaPay.TransaccionDTO[] GetPaymentHistoryResult) {
            this.GetPaymentHistoryResult = GetPaymentHistoryResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CreateBranchPaymentRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CreateBranchPayment", Namespace="http://tempuri.org/", Order=0)]
        public SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentRequestBody Body;
        
        public CreateBranchPaymentRequest() {
        }
        
        public CreateBranchPaymentRequest(SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CreateBranchPaymentRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string user_rut;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public decimal total;
        
        public CreateBranchPaymentRequestBody() {
        }
        
        public CreateBranchPaymentRequestBody(string user_rut, decimal total) {
            this.user_rut = user_rut;
            this.total = total;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CreateBranchPaymentResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CreateBranchPaymentResponse", Namespace="http://tempuri.org/", Order=0)]
        public SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentResponseBody Body;
        
        public CreateBranchPaymentResponse() {
        }
        
        public CreateBranchPaymentResponse(SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CreateBranchPaymentResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string CreateBranchPaymentResult;
        
        public CreateBranchPaymentResponseBody() {
        }
        
        public CreateBranchPaymentResponseBody(string CreateBranchPaymentResult) {
            this.CreateBranchPaymentResult = CreateBranchPaymentResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ImaginaPaySoapChannel : SistemaLibreriaImagina.wsImaginaPay.ImaginaPaySoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ImaginaPaySoapClient : System.ServiceModel.ClientBase<SistemaLibreriaImagina.wsImaginaPay.ImaginaPaySoap>, SistemaLibreriaImagina.wsImaginaPay.ImaginaPaySoap {
        
        public ImaginaPaySoapClient() {
        }
        
        public ImaginaPaySoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ImaginaPaySoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ImaginaPaySoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ImaginaPaySoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int CreatePayment(decimal total, long pedido_id, long metodo_pago_id, long usuario_id) {
            return base.Channel.CreatePayment(total, pedido_id, metodo_pago_id, usuario_id);
        }
        
        public System.Threading.Tasks.Task<int> CreatePaymentAsync(decimal total, long pedido_id, long metodo_pago_id, long usuario_id) {
            return base.Channel.CreatePaymentAsync(total, pedido_id, metodo_pago_id, usuario_id);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsResponse SistemaLibreriaImagina.wsImaginaPay.ImaginaPaySoap.GetPaymentDetails(SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsRequest request) {
            return base.Channel.GetPaymentDetails(request);
        }
        
        public SistemaLibreriaImagina.wsImaginaPay.TransaccionDTO GetPaymentDetails(long id) {
            SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsRequest inValue = new SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsRequest();
            inValue.Body = new SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsRequestBody();
            inValue.Body.id = id;
            SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsResponse retVal = ((SistemaLibreriaImagina.wsImaginaPay.ImaginaPaySoap)(this)).GetPaymentDetails(inValue);
            return retVal.Body.GetPaymentDetailsResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsResponse> SistemaLibreriaImagina.wsImaginaPay.ImaginaPaySoap.GetPaymentDetailsAsync(SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsRequest request) {
            return base.Channel.GetPaymentDetailsAsync(request);
        }
        
        public System.Threading.Tasks.Task<SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsResponse> GetPaymentDetailsAsync(long id) {
            SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsRequest inValue = new SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsRequest();
            inValue.Body = new SistemaLibreriaImagina.wsImaginaPay.GetPaymentDetailsRequestBody();
            inValue.Body.id = id;
            return ((SistemaLibreriaImagina.wsImaginaPay.ImaginaPaySoap)(this)).GetPaymentDetailsAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryResponse SistemaLibreriaImagina.wsImaginaPay.ImaginaPaySoap.GetPaymentHistory(SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryRequest request) {
            return base.Channel.GetPaymentHistory(request);
        }
        
        public SistemaLibreriaImagina.wsImaginaPay.TransaccionDTO[] GetPaymentHistory(string user_rut) {
            SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryRequest inValue = new SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryRequest();
            inValue.Body = new SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryRequestBody();
            inValue.Body.user_rut = user_rut;
            SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryResponse retVal = ((SistemaLibreriaImagina.wsImaginaPay.ImaginaPaySoap)(this)).GetPaymentHistory(inValue);
            return retVal.Body.GetPaymentHistoryResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryResponse> SistemaLibreriaImagina.wsImaginaPay.ImaginaPaySoap.GetPaymentHistoryAsync(SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryRequest request) {
            return base.Channel.GetPaymentHistoryAsync(request);
        }
        
        public System.Threading.Tasks.Task<SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryResponse> GetPaymentHistoryAsync(string user_rut) {
            SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryRequest inValue = new SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryRequest();
            inValue.Body = new SistemaLibreriaImagina.wsImaginaPay.GetPaymentHistoryRequestBody();
            inValue.Body.user_rut = user_rut;
            return ((SistemaLibreriaImagina.wsImaginaPay.ImaginaPaySoap)(this)).GetPaymentHistoryAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentResponse SistemaLibreriaImagina.wsImaginaPay.ImaginaPaySoap.CreateBranchPayment(SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentRequest request) {
            return base.Channel.CreateBranchPayment(request);
        }
        
        public string CreateBranchPayment(string user_rut, decimal total) {
            SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentRequest inValue = new SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentRequest();
            inValue.Body = new SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentRequestBody();
            inValue.Body.user_rut = user_rut;
            inValue.Body.total = total;
            SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentResponse retVal = ((SistemaLibreriaImagina.wsImaginaPay.ImaginaPaySoap)(this)).CreateBranchPayment(inValue);
            return retVal.Body.CreateBranchPaymentResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentResponse> SistemaLibreriaImagina.wsImaginaPay.ImaginaPaySoap.CreateBranchPaymentAsync(SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentRequest request) {
            return base.Channel.CreateBranchPaymentAsync(request);
        }
        
        public System.Threading.Tasks.Task<SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentResponse> CreateBranchPaymentAsync(string user_rut, decimal total) {
            SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentRequest inValue = new SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentRequest();
            inValue.Body = new SistemaLibreriaImagina.wsImaginaPay.CreateBranchPaymentRequestBody();
            inValue.Body.user_rut = user_rut;
            inValue.Body.total = total;
            return ((SistemaLibreriaImagina.wsImaginaPay.ImaginaPaySoap)(this)).CreateBranchPaymentAsync(inValue);
        }
    }
}
