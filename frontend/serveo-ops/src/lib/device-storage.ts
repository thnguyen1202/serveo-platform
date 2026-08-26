const DEVICE_ID_KEY = 'serveo_ops_device_id';

export const deviceStorage = {
  getDeviceId(): string {
    let deviceId = localStorage.getItem(DEVICE_ID_KEY);

    if (!deviceId) {
      deviceId = 'ops-' + crypto.randomUUID();
      localStorage.setItem(DEVICE_ID_KEY, deviceId);
    }

    return deviceId;
  },

  getClientType() {
    const clientType = 1;
    return clientType;
  },

  clear() {
    localStorage.removeItem(DEVICE_ID_KEY);
  },
};
