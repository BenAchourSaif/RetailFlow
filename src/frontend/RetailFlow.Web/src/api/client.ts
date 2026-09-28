const API_URL = import.meta.env.VITE_API_URL;

const TENANT_ID =
    import.meta.env.VITE_TENANT_ID;

async function request<T>(
    endpoint: string,
    options: RequestInit = {}
): Promise<T> {
    const headers = new Headers(options.headers);

    headers.set("Content-Type", "application/json");

    if (TENANT_ID) {
        headers.set("X-Tenant-Id", TENANT_ID);
    }

    const response = await fetch(
        `${API_URL}${endpoint}`,
        {
            ...options,
            headers,
        }
    );

    if (!response.ok) {
        const message = await response.text();

        throw new Error(
            message || `API error: ${response.status}`
        );
    }

    if (response.status === 204) {
        return undefined as T;
    }

    return response.json() as Promise<T>;
}

export function apiGet<T>(
    endpoint: string
): Promise<T> {
    return request<T>(endpoint);
}

export function apiPost<TRequest, TResponse>(
    endpoint: string,
    body: TRequest
): Promise<TResponse> {
    return request<TResponse>(
        endpoint,
        {
            method: "POST",
            body: JSON.stringify(body),
        }
    );
}