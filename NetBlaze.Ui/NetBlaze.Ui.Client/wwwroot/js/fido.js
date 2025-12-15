window.fido = (function () {
  function b64ToBuf(b64) {
    const binStr = atob(b64.replace(/-/g, '+').replace(/_/g, '/'));
    const buf = new Uint8Array(binStr.length);
    for (let i = 0; i < binStr.length; i++) buf[i] = binStr.charCodeAt(i);
    return buf.buffer;
  }
  function bufToB64(buf) {
    const bytes = new Uint8Array(buf);
    let bin = '';
    for (let i = 0; i < bytes.byteLength; i++) bin += String.fromCharCode(bytes[i]);
    return btoa(bin).replace(/\+/g, '-').replace(/\//g, '_').replace(/=+$/, '');
  }
  async function createCredential(options) {
    const pubKey = {
      rp: options.rp,
      user: {
        id: b64ToBuf(options.user.id),
        name: options.user.name,
        displayName: options.user.displayName
      },
      challenge: b64ToBuf(options.challenge),
      pubKeyCredParams: options.pubKeyCredParams,
      authenticatorSelection: options.authenticatorSelection
    };
    const cred = await navigator.credentials.create({ publicKey: pubKey });
    return {
      id: cred.id,
      rawId: bufToB64(cred.rawId),
      type: cred.type,
      response: {
        clientDataJSON: bufToB64(cred.response.clientDataJSON),
        attestationObject: bufToB64(cred.response.attestationObject)
      }
    };
  }
  async function getAssertion(options) {
    const pubKey = {
      challenge: b64ToBuf(options.challenge),
      rpId: options.rpId,
      userVerification: options.userVerification || 'preferred'
    };
    const res = await navigator.credentials.get({ publicKey: pubKey });
    return {
      id: res.id,
      rawId: bufToB64(res.rawId),
      type: res.type,
      response: {
        clientDataJSON: bufToB64(res.response.clientDataJSON),
        authenticatorData: bufToB64(res.response.authenticatorData),
        signature: bufToB64(res.response.signature),
        userHandle: res.response.userHandle ? bufToB64(res.response.userHandle) : null
      }
    };
  }
  return { createCredential, getAssertion };
})();
