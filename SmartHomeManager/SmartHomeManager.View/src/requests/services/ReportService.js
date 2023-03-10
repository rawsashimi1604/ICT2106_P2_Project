import HttpService from "requests/HttpService";
import PdfDownloadService from "requests/PdfDownloadService"

const GET_DEVICE_REPORT_API_ROUTE = `https://localhost:7140/api/analysis/device/download`
const GET_DEVICES_BY_GUID_ROUTE = `https://localhost:7140/api/analysis/device/getDevicesByGUID`

function getDeviceReport(){
    return PdfDownloadService.get(GET_DEVICE_REPORT_API_ROUTE);
}

function getDevicesByGUID(){
    return HttpService.get(GET_DEVICES_BY_GUID_ROUTE)
}

export default{
    getDeviceReport,
    getDevicesByGUID,
}