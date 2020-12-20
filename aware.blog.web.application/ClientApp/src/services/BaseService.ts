export function getJsonHeaders() {
    let headers = new Headers();
    headers.append('content-type', 'application/json');
    headers.append('accept', 'application/json');

    return headers;
}

export async function fetchJson<TResult>(
    input: RequestInfo,
    init?: RequestInit): Promise<TResult> {
    let response = await fetch(input, {
        ...init,
        headers: getJsonHeaders(),
        credentials: "include"
    });

    let responseJson = await response.json();

    return responseJson as TResult;
}

export async function fetchAny(
    input: RequestInfo,
    init?: RequestInit): Promise<void> {
    await fetch(input, {
        ...init,
        headers: getJsonHeaders(),
        credentials: "include"
    });
}