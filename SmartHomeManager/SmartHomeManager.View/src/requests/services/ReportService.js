import HttpService from "requests/HttpService";
import PdfDownloadService from "requests/PdfDownloadService"

const GET_DEVICE_REPORT_API_ROUTE = `https://localhost:7140/api/analysis/device/download/`
const GET_DEVICES_BY_GUID_ROUTE = `https://localhost:7140/api/analysis/device/`
const GET_HOUSEHOLD_REPORT_ROUTE = `https://localhost:7140/api/analysis/householdReport/download/`

function getDeviceReport(deviceId, lastMonths) {
    return PdfDownloadService.get(`${GET_DEVICE_REPORT_API_ROUTE}${deviceId}/${lastMonths}`);
}

function getDevicesByGUID(accountId) {
    return HttpService.get(GET_DEVICES_BY_GUID_ROUTE + accountId);
}

function getHouseholdReport(accountId, lastMonths) {
    return PdfDownloadService.get(GET_HOUSEHOLD_REPORT_ROUTE + accountId + "/" + lastMonths)
}

export default {
    getDeviceReport,
    getDevicesByGUID,
    getHouseholdReport,

}