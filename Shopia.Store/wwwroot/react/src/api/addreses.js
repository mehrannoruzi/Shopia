const baseUrl = 'https://localhost:44328/';
const addr = {
    getSingleStore:`${baseUrl}Store/GetSingle`,
    getProducts:`${baseUrl}Product/Get`,
    getSingleProduct:`${baseUrl}Product/GetSingle`,
    postCompleteInfo:`${baseUrl}order/CompleteInfo`,
    getAddresses:`${baseUrl}Address/Get`,
    postOrder:`${baseUrl}order/submit`
} 
export default addr;