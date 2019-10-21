import { IArrangement } from './models/arrangement.model';
import axios from 'axios';

export class ArrangementsService {
    public static getRandom(count: number): Promise<IArrangement[]> {
        return new Promise((resolve, reject) =>
            axios
                .get<IArrangement[]>('/api/arrangements')
                .then(response => resolve(response.data))
                .catch(error => reject(error))
        );
    }
}
