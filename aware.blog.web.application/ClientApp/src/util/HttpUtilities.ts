import queryString from 'query-string';

export function buildQueryParameterString(params: { [key: string]: any }) {
    return queryString.stringify(params);
}

export function parseQueryString<T>(query: string) {
    return queryString.parse(query, {
        parseNumbers: true
    }) as unknown as T;
}