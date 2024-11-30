import { API_BASE_URL } from "./constants";


export const resolvePath = (path: string) => {
    if(!path){
        throw new Error('Path is required');
    }
    if(path.startsWith('/')){
        return `${API_BASE_URL}${path}`;
    }
    return `${API_BASE_URL}/${path}`;
}

export interface Result<T> {
    data: T;
    error: string;
}