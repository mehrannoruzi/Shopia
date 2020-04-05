import audioImage from './../assets/images/FileTypes/audio.png';
import videoImage from './../assets/images/FileTypes/video.png';
import archiveImage from './../assets/images/FileTypes/archive.png';
import documentImage from './../assets/images/FileTypes/document.png';
import unknownImage from './../assets/images/FileTypes/unknown.png';

export const fileTypes = {
    Unknown: { id: 0, type: 'application/octet-stream' },
    Image: { id: 1, type: 'image/png' },
    Document: { id: 2, type: 'application/vnd.openxmlformats-officedocument.wordprocessingml.document' },
    Archive: { id: 3, type: 'application/zip' },
    Audio: { id: 4, type: 'audio/mpeg' },
    Video: { id: 5, type: 'video/mp4' }
};

export  function getFileType(fileName) {
    let ext = fileName.toLowerCase().split('.').reverse()[0];
    switch (ext) {
        case "png":
        case "jpg":
        case "jpeg":
        case "gif":
        case "tiff":
            return fileTypes.Image;
        case "mp3":
        case "wav":
        case "flm":
        case "fsm":
        case "ogg":
        case "m4a":
        case "m4b":
        case "m4p":
        case "m4r":
            return fileTypes.Audio;
        case "mp4":
        case "mkv":
        case "avi":
        case "ts":
        case "m4v":
        case "flv":
            return fileTypes.Video;
        case "zip":
        case "rar":
        case "iso":
        case "tar":
        case "jar":
            return fileTypes.Archive;
        case "pdf":
        case "doc":
        case "docx":
        case "txt":
        case "xls":
        case "xlsx":
        case "josn":
        case "pptx":
            return fileTypes.Document;
        default:
            return fileTypes.Unknown;
    }
};

export  function getDefaultImageUrl(fileName) {
    let ext = fileName.toLowerCase().split('.').reverse()[0];
    switch (getFileType(ext).id) {
        case fileTypes.Image:
            return null;
        case fileTypes.Audio.id:
            return audioImage;
        case fileTypes.Video.id:
            return videoImage;
        case fileTypes.Archive.id:
            return archiveImage;
        case fileTypes.Document.id:
            return documentImage;
        default:
            return unknownImage;
    }
};