import HttpService from "requests/HttpService";
import PdfDownloadService from "requests/PdfDownloadService"

const GET_DEVICE_REPORT_API_ROUTE = `https://localhost:7140/api/analysis/device/download/`
const GET_DEVICES_BY_GUID_ROUTE = `https://localhost:7140/api/analysis/device/`

function getDeviceReport(deviceId){
    return PdfDownloadService.get(GET_DEVICE_REPORT_API_ROUTE + deviceId);
}

function getDevicesByGUID(accountId){
    return HttpService.get(GET_DEVICES_BY_GUID_ROUTE + accountId);
}

export default{
    getDeviceReport,
    getDevicesByGUID,
}